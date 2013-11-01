using System;
using System.Xml;
using System.IO;
using ASMDM;

namespace LocalMachine
{
	/// <summary>
	/// Summary description for LocalMachine.
	/// </summary>
	public class LocalMachine:ISystemBus,IOutputTerminal,IInputTerminal
	{

		private ASMProcessor localprocessor;
		public static int matrixmemsize;
		public static int threadstacksize;
		
		private IExternalDevicePort[] externaldevices;

		public void Initialize(){
			localprocessor = new ASMProcessor();
			LoadProcessorsAndConfigurations();
		}
		private void LoadProcessorsAndConfigurations(){
			XmlDocument cfg = new XmlDocument();
			cfg.Load(Loader.apppath +  @"\config.xml");
			
			matrixmemsize = int.Parse(cfg.SelectSingleNode("Configurations/MatrixMemorySize").InnerText);
			threadstacksize = int.Parse(cfg.SelectSingleNode("Configurations/ThreadStackSize").InnerText);

			//*********************Loading Processors************
			XmlNodeList nlist = cfg.SelectNodes("Configurations/RemoteProcessor");

			Console.WriteLine("Loading RemoteProcessors");
			foreach(XmlNode tmp in nlist){
				try
				{
					RemoteProcessor tmpprocessor = new  RemoteProcessor();
					string ipaddr = tmp.SelectSingleNode("IPAddress").InnerText;
					int prt = int.Parse(tmp.SelectSingleNode("Port").InnerText);
					Console.WriteLine("Connecting to {0}:{1}....",ipaddr,prt);
					tmpprocessor.Connect(ipaddr,prt);
					tmpprocessor.StartProcessor();
					tmpprocessor.localprocessor = localprocessor;
					RemoteProcessorsManager.AddProcessor(tmpprocessor);
				}
				catch(Exception exc){
					Console.WriteLine(exc.Message);
				}
			}

			//*********************Loading External Devices************
			nlist = cfg.SelectNodes("Configurations/ExternalDevice");

			if(nlist.Count>0)
			{
				Console.WriteLine("Loading External Devices");
				externaldevices = new ExternalDevicePort[nlist.Count];
				int tmp2=0;
				foreach(XmlNode tmp in nlist)
				{
					externaldevices[tmp2] = new ExternalDevicePort(
						tmp.SelectSingleNode("Name").InnerText
						,new ExternalDevicePort.Echo(this.EchoMessage));
					externaldevices[tmp2].Initialize(int.Parse(
						tmp.SelectSingleNode("Port").InnerText));
					tmp2++;
				}
			}

		}

		private ProgramCode LoadFile(string filename)
		{
		
			BinaryReader reader = new BinaryReader(new FileStream(filename,FileMode.Open,FileAccess.Read));

			ushort filecode = reader.ReadUInt16();

			if(filecode==1508)
			{
				ProgramCode asmcode = new ProgramCode();
				asmcode.memory = new double[reader.ReadInt32(),2];
				
				int nmlen = reader.ReadInt32();
				asmcode.numconstants = new double[nmlen/8];
				Buffer.BlockCopy(reader.ReadBytes(nmlen),0,asmcode.numconstants,0,nmlen);

				nmlen = reader.ReadInt32();

				string literal = System.Text.ASCIIEncoding.ASCII.GetString(reader.ReadBytes(nmlen));
                
				asmcode.literals = literal.Split(new char[]{'\x00'});
				
				nmlen = reader.ReadInt32();
				asmcode.instructions = new ushort[nmlen/2];
				Buffer.BlockCopy(reader.ReadBytes(nmlen),0,asmcode.instructions,0,nmlen);

				reader.Close();
				return asmcode;
		
			}
			else
				throw new Exception("It is not a valid File.");			
		}
		public void ProcessFile(string filename){
			
			Console.WriteLine("Loading Program --- {0}.....",filename);
			localprocessor.currentcode = LoadFile(filename);
			
			localprocessor.currentcode.matrixmemory = new ASMMath.Matrix[matrixmemsize];
			localprocessor.currentcode.bus = this;			

            ProgramThread mthread = new ProgramThread();
			mthread.inspointer = 0;
			mthread.endthread = localprocessor.currentcode.instructions.Length-1;
			mthread.stackpointer = -1;
			mthread.pstack = new double[threadstacksize,2];
			
			RemoteProcessorsManager.SendNumConstants(ref localprocessor.currentcode);
			RemoteProcessorsManager.SendLiterals(ref localprocessor.currentcode);

			Console.WriteLine("Start running....");
			localprocessor.raiseerror = new RaiseError(this.ErrorEvent);
			localprocessor.Process(ref mthread);
		}
		public void ErrorEvent(string errormsg){
			Console.WriteLine ("Error Occured - " + errormsg);
			Close();
		}
		public void Output(string data){
			Console.Write(data);				
		}
		public void EchoMessage(string message){
			Console.WriteLine(message);
		}
		public double Input(string message){
			try
			{
				Console.Write(message);
				return double.Parse(Console.ReadLine());
			}
			catch(Exception e){
				return 0;							
			}
		}
		public IOutputTerminal Out
		{
			get{
				return this;
			}
		}
		public IInputTerminal In
		{
			get
			{
				return this;
			}
		}
		public IExternalDevicePort[] ExternalDevices
		{
			get{
				return externaldevices;
			}
		}
		public void Close(){
			RemoteProcessorsManager.RemoveAllProcessors();
			localprocessor.currentcode.matrixmemory = null;
			localprocessor.currentcode.memory = null;
			localprocessor.currentcode.numconstants = null;
			localprocessor.currentcode.literals  = null;
			localprocessor.currentcode.instructions = null;
			localprocessor.currentcode.bus = null;

			if(externaldevices !=null)
			{
				foreach(IExternalDevicePort	tmp in	externaldevices)
				{
					tmp.ClosePort();
				}
				externaldevices = null;
			}
		}

	}
	
}
