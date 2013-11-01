using System;
using System.Threading;
using System.Xml;
using System.Net;
using ASMDM;

namespace RemoteMachine
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	enum ServerThreadStates
	{
		START,STOP,CONNECTED,EXIT,RESTART
	}
	class RemoteMachine 
	{
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static RemoteRequestServer server;
		static Thread serverthread;
		static ServerThreadStates  serverstate;
		static bool manualstop;
		public static int threadstacksize;
		
		static void Main(string[] args)
		{
						
			manualstop = false;
			serverstate = ServerThreadStates.STOP;
			serverthread =  new Thread(new ThreadStart(ThreadProc));
			serverthread.Start();
			int v;
			do
			{
				Console.WriteLine("1 - Start Processor");
				Console.WriteLine("2 - Stop Processor");
				Console.WriteLine("3 - Restart Processor");
				Console.WriteLine("4 - Exit");
			
				try
				{
					v = int.Parse(Console.ReadLine());
				}
				catch(Exception e){
					v=0;				
				}
				
				switch(v){
					case 1:
						if(serverstate==ServerThreadStates.STOP){
							Console.WriteLine("Starting Server.....");
							serverstate = ServerThreadStates.START;
							serverthread.Resume();
						}else
							Console.WriteLine("Server is already started.");
						break;
						
					case 2:
						if(serverstate!=ServerThreadStates.STOP){
							Console.WriteLine("Stopping Server.....");
							serverstate = ServerThreadStates.STOP;
							manualstop = true;
							server.Stop();
						}else
							Console.WriteLine("Server is not running.");
						break;
					
					case 3:
						if(serverstate !=ServerThreadStates.STOP){
							Console.WriteLine("Restarting Server....");
							serverstate = ServerThreadStates.RESTART;
							manualstop = true;
							server.Stop();
						}else
							Console.WriteLine("Server is not running.");
						break;
					
					case 4:
						if(serverstate !=ServerThreadStates.STOP)
						{
							Console.WriteLine("Stopping Server.....");
							serverstate = ServerThreadStates.EXIT;
							manualstop = true;
							server.Stop();
						}
						else
						{
							serverstate = ServerThreadStates.EXIT;
							serverthread.Resume();
						}
						break;

					default:
						Console.WriteLine("Bad Command");
						break;
				}
				Thread.Sleep(1000);
			}while(v!=4);

			serverthread = null;
		}
		public static void InitializeProcessor(){
			XmlDocument cfg = new XmlDocument();
			cfg.Load("remoteconfig.xml");
			
			//*********************Loading External Devices************
			XmlNodeList nlist = cfg.SelectNodes("ProcessorConfiguration/ExternalDevice");
			IExternalDevicePort[] externaldevices = null;
			if(nlist.Count>0)
			{
				Console.WriteLine("Loading External Devices");
				externaldevices = new ExternalDevicePort[nlist.Count];
				int tmp2=0;
				foreach(XmlNode tmp in nlist)
				{
					externaldevices[tmp2] = new ExternalDevicePort(
						tmp.SelectSingleNode("Name").InnerText
						,new ExternalDevicePort.Echo(EchoMessage));
					externaldevices[tmp2].Initialize(int.Parse(
						tmp.SelectSingleNode("Port").InnerText));
					tmp2++;
				}
			}


			server = new RemoteRequestServer(int.Parse(cfg.SelectSingleNode(
				"ProcessorConfiguration/Port").InnerText),
				cfg.SelectSingleNode("ProcessorConfiguration/Name").InnerText,externaldevices);
			
			threadstacksize =int.Parse(cfg.SelectSingleNode(
				"ProcessorConfiguration/ThreadStackSize").InnerText);

			
		}
		public static void EchoMessage(string message)
		{
			Console.WriteLine(message);
		}
		public static void ThreadProc(){
		
			while(serverstate != ServerThreadStates.EXIT ) 
			{
				switch(serverstate){

					case ServerThreadStates.CONNECTED:
						Console.WriteLine("Recieving data packet....");
						try
						{
							server.RecievePacket();						
						}
						catch (Exception e){
							Console.WriteLine(e.Message);
							if(!manualstop)
							{
								Console.WriteLine("Restarting Server....");
								serverstate = ServerThreadStates.RESTART;
							}
							else
								manualstop=false;
						}
						break;
					
					case ServerThreadStates.START:
						InitializeProcessor();
						Console.WriteLine("Processor Name : " + server.processorname);
						Console.WriteLine("Listening on port : " + server.localprt.ToString());
						if(server.Start())
						{
							serverstate = ServerThreadStates.CONNECTED;
							Console.WriteLine("Connected to : " + ((IPEndPoint) server.listenersocket.RemoteEndPoint).Address.ToString());
						}
						else
							if(!manualstop)
							{
								Console.WriteLine("Restarting Server....");
								serverstate = ServerThreadStates.RESTART;
							}else
								manualstop=false;
						break;
					
					case ServerThreadStates.STOP:
						if(!Object.ReferenceEquals(server,null))
						{
							server.Stop();
							server=null;
							Console.WriteLine("Server has been stopped.");
						}
						Thread.CurrentThread.Suspend();
						break;

					case ServerThreadStates.RESTART:
						if(!Object.ReferenceEquals(server,null)){
							server.Stop();
							server=null;
						}
						serverstate = ServerThreadStates.START;
						break;
				}			
			}
			server = null;
		}
		
		
	}
}
