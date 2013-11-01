using System;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using ASMDM;

namespace LocalMachine
{
	/// <summary>
	/// Summary description for RemoteProcessorsManager.
	/// </summary>
	public class RemoteProcessorsManager
	{
	
		public static ArrayList FreeProcessors = new ArrayList();
		public static ArrayList BusyProcessors = new ArrayList();

		public static RemoteProcessor PopProcessor(){
			lock(typeof(RemoteProcessorsManager)){
				RemoteProcessor tmp = (RemoteProcessor)FreeProcessors[FreeProcessors.Count-1];
				FreeProcessors.Remove(tmp);
				BusyProcessors.Add(tmp);
				return tmp;
			}
		}
		public static void FreeProcessor(RemoteProcessor processor){
			lock(typeof(RemoteProcessorsManager))
			{
				BusyProcessors.Remove(processor);
				FreeProcessors.Add(processor);
			}
		}
		public static void RemoveAllProcessors(){
			lock(typeof(RemoteProcessorsManager))
			{
				foreach(RemoteProcessor tmp in FreeProcessors)
				{
					tmp.Disconnect();
					tmp.StopProcessor();
					//FreeProcessors.Remove(tmp);
				}
				foreach(RemoteProcessor tmp in BusyProcessors)
				{
					tmp.Disconnect();
					tmp.StopProcessor();
					//BusyProcessors.Remove(tmp);
				}
			}
		}
		public static void CloseProcessor(RemoteProcessor processor){
			if(FreeProcessors.Contains(processor))FreeProcessors.Remove(processor);
			if(BusyProcessors.Contains(processor))BusyProcessors.Remove(processor);
		}
		public static void AddProcessor(RemoteProcessor processor){
			FreeProcessors.Add(processor);
		}
		public static void SendNumConstants(ref ProgramCode asmcode){
			
			byte[] packet = new byte[9+Buffer.ByteLength(asmcode.numconstants)];
			Buffer.BlockCopy(BitConverter.GetBytes(asmcode.matrixmemory.Length),0,packet,
								5,4);
			packet[4] = (byte)RemoteProtocolMsgs.NUMCOS;
			Buffer.BlockCopy(asmcode.numconstants,0,packet,9,Buffer.ByteLength(asmcode.numconstants));
			Buffer.BlockCopy(BitConverter.GetBytes(packet.Length-4),0,packet,0,4);
			
			foreach(RemoteProcessor tmprp in FreeProcessors)
				tmprp.SendPacket(packet);
		}
		public static void SendLiterals(ref ProgramCode asmcode)
		{
			if(asmcode.literals.Length>0)
			{
				string literals = String.Join("\x00",asmcode.literals);
			
				byte[] packet = new byte[5+System.Text.ASCIIEncoding.ASCII.GetByteCount(literals)];
				packet[4] = (byte)RemoteProtocolMsgs.LITERALS;
						
				Buffer.BlockCopy(System.Text.ASCIIEncoding.ASCII.GetBytes(literals)
					,0,packet,5,System.Text.ASCIIEncoding.ASCII.GetByteCount(literals));
				Buffer.BlockCopy(BitConverter.GetBytes(packet.Length-4),0,packet,0,4);
			
				foreach(RemoteProcessor tmprp in FreeProcessors)
					tmprp.SendPacket(packet);
			}
		}
		/*public static Socket GetRemoteProcessor(){
			Socket tempsock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			
			string tmpIp="192.168.0.1";
			int prt=10000;
			tempsock.Connect(new IPEndPoint(IPAddress.Parse(tmpIP),prt));

			return tempsock;
		}*/
	}
}
