using System;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using ASMMath;
using ASMDM;

namespace RemoteMachine
{
	/// <summary>
	/// Summary description for RemoteRequestServer.
	/// </summary>
	//public enum RemoteProtocolMsgs
	//{
	//	START,NUMCOS,END,ERROR,GETMATRIX,RECIEVEMATRIX,LITERALS
	//}
	public class RemoteRequestServer :ISystemBus,IOutputTerminal,IInputTerminal
	{

		public Socket listenersocket;
		NetworkStream mystream;
		public int localprt;
		public ASMProcessorRS myprocessor;
		public string processorname;
		public int matrixmemsize=0;
		
		private IExternalDevicePort[] externaldevices;
		
		private ProgramThread mthread;
		byte[] packet;
		private ArrayList memlist;
        
		public RemoteRequestServer(int localprt,string pname,IExternalDevicePort[] externaldevices)
		{
			this.externaldevices = externaldevices;
			this.localprt = localprt;
			this.processorname = pname;
			matrixmemsize=20;
		}
		public bool Start(){
			try
			{
				listenersocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			
				listenersocket.Bind(new IPEndPoint(IPAddress.Any,localprt));
				listenersocket.Listen(1);
			
				Socket temps =  listenersocket.Accept();
				listenersocket.Close();
				listenersocket = temps;

				Initialize();
				return true;
			}
			catch(SocketException e){
				Console.WriteLine(e.Message);
				return false;
			}
		}
		private void Initialize(){
			
			mystream = new NetworkStream(listenersocket,true);
			myprocessor = new ASMProcessorRS(new GetMatrixFromFar(this.GetMatrixIntoMemory));
			myprocessor.raiseerror = new RaiseError(this.SendError);
			myprocessor.currentcode.matrixmemory = new Matrix[matrixmemsize];
			myprocessor.currentcode.matrixstate = new MatrixState[matrixmemsize];
			myprocessor.currentcode.literals = new string[0];
			myprocessor.currentcode.bus = this;
			memlist = new ArrayList();
			mthread = new ProgramThread();
			mthread.pstack = new double[RemoteMachine.threadstacksize,2];

		}
		public void Stop(){
			if(externaldevices !=null)
			{
				foreach(IExternalDevicePort	tmp in	externaldevices)
					tmp.ClosePort();
				externaldevices = null;
			}

			if(!Object.ReferenceEquals(mystream,null))
			{
				mystream.Close();
				mystream = null;
				listenersocket = null;
			}
			else if(!Object.ReferenceEquals(listenersocket,null)){
				listenersocket.Close();
				listenersocket = null;
			}

			myprocessor =null;
			packet=null;
			memlist =null;
			mthread.pstack = null;
		}
	
		public void GetMatrixIntoMemory(int loc){
			packet = new byte[9];
			packet[0] = 5;
			packet[4] = (byte)RemoteProtocolMsgs.GETMATRIX;
			Buffer.BlockCopy(BitConverter.GetBytes(loc),0,packet,5,4);
			mystream.Write(packet,0,packet.Length);
			RecievePacket();
		}
		public void RecievePacket()
		{
			packet = new byte[4];
			int tmp = mystream.Read(packet,0,4);
			if(tmp==0)throw new Exception("Connection has been closed from the remote side.");
			int packetsize = BitConverter.ToInt32(packet,0);
			packet = new byte[packetsize];
			int bytesread=0;
			while(bytesread<packetsize)
			{
				tmp = mystream.Read(packet,bytesread,packetsize-bytesread);
				if(tmp==0)throw new Exception("Connection has been closed from the remote side.");
				bytesread +=tmp;
			}
			ProcessPacket();
		}
		private void ProcessPacket()
		{
			int pointer=0;
			switch(packet[0]){
			
				case (byte)RemoteProtocolMsgs.START:
					Console.WriteLine("Start processing request.");
					myprocessor.currentcode.instructions = new ushort[
												BitConverter.ToInt32(packet,1)];
					Buffer.BlockCopy(packet,5,myprocessor.currentcode.instructions,
										0,myprocessor.currentcode.instructions.Length*2);
					pointer=myprocessor.currentcode.instructions.Length*2+5;
					ProcessMemory(ref pointer);
					mthread.inspointer = 0;
					mthread.endthread = myprocessor.currentcode.instructions.Length-1;
					mthread.stackpointer = -1;
					mthread.cmpflag = false;
					
					try
					{
						myprocessor.Process(ref mthread);
						SendRequestResponse();
						Console.WriteLine("Request response sent.");
					}
					catch(Exception e){
                        SendError(e.Message);					
					}
					myprocessor.currentcode.matrixstate = new MatrixState[matrixmemsize];
					myprocessor.currentcode.matrixmemory = new Matrix[matrixmemsize];
					break;

				case (byte)RemoteProtocolMsgs.NUMCOS:
					matrixmemsize = BitConverter.ToInt32(packet,1);
					myprocessor.currentcode.matrixmemory = new Matrix[matrixmemsize];
					myprocessor.currentcode.matrixstate = new MatrixState[matrixmemsize];

					myprocessor.currentcode.numconstants = new double[
											(packet.Length-5)/8];
					Buffer.BlockCopy(packet,5,myprocessor.currentcode.numconstants,
																0,packet.Length-5);
					Console.WriteLine("Numeric constants table updated.");
					break;
				case (byte)RemoteProtocolMsgs.LITERALS:
					string literals = System.Text.ASCIIEncoding.ASCII.GetString(packet,1,packet.Length-1);
					myprocessor.currentcode.literals = literals.Split(new char[]{'\x00'});
					Console.WriteLine("Literal constants table updated.");
					break;
				case (byte)RemoteProtocolMsgs.RECIEVEMATRIX:
					{
						int loc=BitConverter.ToInt32(packet,1);
						int rw = BitConverter.ToInt32(packet,5);
						int cl = BitConverter.ToInt32(packet,9);
						
						myprocessor.currentcode.matrixmemory[loc] = new Matrix(rw,cl);
						Buffer.BlockCopy(packet,13,
							myprocessor.currentcode.matrixmemory[loc].m_MatArr,0,rw*cl*8);
						myprocessor.currentcode.matrixstate[loc]= MatrixState.AVAILABLE;
					}
					break;
			}							
		}
		public void SendError(string message){
			packet = new byte[System.Text.ASCIIEncoding.ASCII.GetByteCount(message)+5];
			Buffer.BlockCopy(BitConverter.GetBytes(System.Text.ASCIIEncoding.ASCII.GetByteCount(message)+1),0,packet,0,4);
			packet[4] = (byte)RemoteProtocolMsgs.ERROR;
			Buffer.BlockCopy(System.Text.ASCIIEncoding.ASCII.GetBytes(message),0,packet,5,System.Text.ASCIIEncoding.ASCII.GetByteCount(message));

			mystream.Write(packet,0,packet.Length);
			Console.WriteLine("Error message sent - " + message);
		}
		private void SendRequestResponse(){
			packet = new byte[1024];
			byte[] tmppacket = new byte[1024];
			packet[4] = (byte)RemoteProtocolMsgs.END;
			
			Buffer.BlockCopy(BitConverter.GetBytes(memlist.Count*11),0,packet,5,4);
			
			int pointer=9;
			if(pointer+memlist.Count*11>=packet.Length)IncreaseCapacity(memlist.Count*11,ref packet);

			int pointer2=0;
			
			foreach(object obj in memlist){
				ushort addr = ushort.Parse(obj.ToString());
				
				Buffer.BlockCopy(BitConverter.GetBytes(addr),0,packet,pointer,2);
				pointer+=2;
				Buffer.BlockCopy(BitConverter.GetBytes(
					myprocessor.currentcode.memory[addr,
					(int)MEMCONSTANTS.DATA]),0,packet,pointer,8);
				pointer+=8;
				packet[pointer] = (byte)myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.TYPE];
				pointer++;

				if(myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.TYPE]==(double)MEMTYPECONSTANTS.REFERENCE){
					
					int matref = (int)myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.DATA];

					if(myprocessor.currentcode.matrixstate[matref]==MatrixState.MODIFIED
					|| myprocessor.currentcode.matrixstate[matref]==MatrixState.OVERWITTEN
					|| myprocessor.currentcode.matrixstate[matref]==MatrixState.NEWMATRIX)
					{
						int blen = Buffer.ByteLength(myprocessor.currentcode.matrixmemory[matref].m_MatArr);

						if(pointer2+blen+13>=tmppacket.Length)IncreaseCapacity(blen+13,ref tmppacket);

						tmppacket[pointer2] = (byte)myprocessor.currentcode.matrixstate[matref];
                        pointer2++;
						if(myprocessor.currentcode.matrixstate[matref]==MatrixState.NEWMATRIX)
						{
							Buffer.BlockCopy(BitConverter.GetBytes(addr),0,tmppacket,pointer2,2);
							pointer2+=2;
						}
						else
						{
							Buffer.BlockCopy(BitConverter.GetBytes(matref),0,tmppacket,pointer2,4);
							pointer2+=4;
						}
						Buffer.BlockCopy(BitConverter.GetBytes(
							myprocessor.currentcode.matrixmemory[matref].Rows),
									0,tmppacket,pointer2,4);
						pointer2+=4;
						Buffer.BlockCopy(BitConverter.GetBytes(
							myprocessor.currentcode.matrixmemory[matref].Columns),
							0,tmppacket,pointer2,4);
						pointer2+=4;
						Buffer.BlockCopy(myprocessor.currentcode.matrixmemory[matref].m_MatArr
							,0,tmppacket,pointer2,blen);
						pointer2+=blen;
					}
				}
			}
			
			if(pointer2+myprocessor.currentcode.matrixstate.Length*5>=tmppacket.Length)IncreaseCapacity(myprocessor.currentcode.matrixstate.Length*5,ref tmppacket);
			
			for(int lpv=0;lpv<myprocessor.currentcode.matrixstate.Length;lpv++){
				if(myprocessor.currentcode.matrixstate[lpv]==MatrixState.REMOVED){
					tmppacket[pointer2] = (byte)MatrixState.REMOVED;
					pointer2++;
					Buffer.BlockCopy(BitConverter.GetBytes(lpv),0,tmppacket,pointer2,4);
					pointer2+=4;
				}
			}

			if(pointer+pointer2>=packet.Length)IncreaseCapacity(pointer2-pointer+1,ref packet);											

			Buffer.BlockCopy(tmppacket,0,packet,pointer,pointer2);
			pointer+=pointer2;
			
			Buffer.BlockCopy(BitConverter.GetBytes(pointer-4),0,packet,0,4);

			mystream.Write(packet,0,pointer);
		}
		private void IncreaseCapacity(int amnt,ref byte[] packet)
		{
			byte[] tmp = new byte[packet.Length +amnt];
			Buffer.BlockCopy(packet,0,tmp,0,packet.Length);
			packet = tmp;
		}
		private void ProcessMemory(ref int offset){
			
			int mbytes = BitConverter.ToInt32(packet,offset);
			offset=offset+4;
			mbytes += offset;
			
			myprocessor.currentcode.memory = new double[
									BitConverter.ToInt16(packet,offset)+1,2];
			offset+=2;

			memlist.Clear();
			
			while(offset<mbytes){
				
				ushort addr = (ushort)BitConverter.ToInt16(packet,offset);
				memlist.Add(addr);
				offset+=2;
				myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.DATA] = 
							BitConverter.ToDouble(packet,offset);
				offset+=8;
				myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.TYPE] = packet[offset];
				if(packet[offset]==(byte)MEMTYPECONSTANTS.REFERENCE)
					myprocessor.currentcode.matrixstate[
						(int)myprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.DATA]]=MatrixState.NOTAVAILABLE;
				offset++;
			}
		}

		public void Output(string data)
		{
			Console.Write(data);		
		}
		public IOutputTerminal Out
		{
			get
			{
				return this;
			}
		}
		public double Input(string message)
		{
			/*try
			{
				Console.Write(message);
				return double.Parse(Console.ReadLine());
			}
			catch(Exception e)
			{*/
			return 0;							
			//}
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
			get
			{
				return externaldevices;
			}
		}
		~RemoteRequestServer(){
			Stop();
		}
	}

}
