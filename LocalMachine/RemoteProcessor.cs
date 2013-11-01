using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using ASMMath;
using ASMDM;


namespace LocalMachine
{
	/// <summary>
	/// Summary description for RemoteRequestSender.
	/// </summary>
	public enum RemoteProcessorStates{
		BLOCKED,RUNNING,STOP,WAITING
	}
	
	//public enum MatrixState
	//{
	//	UNUSED,NOTAVAILABLE,AVAILABLE,REMOVED,NEWMATRIX,MODIFIED,OVERWITTEN
	//}
	public class RemoteProcessor
	{
		
		public ASMProcessor localprocessor;
		public ProgramThread mthread;
		public int memstart,memend;
		public Thread thread;
		byte[] packet;
		public RemoteProcessorStates state;
		

		private Thread joinedthread;
		private bool joinflag=false;
				
		Socket mysocket;
		NetworkStream mystream;

		public RemoteProcessor()
		{
			mthread = new ProgramThread();
			state = RemoteProcessorStates.STOP;
		}
		public void Connect(string ipaddr,int prt){
			mysocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
			mysocket.Connect(new IPEndPoint(IPAddress.Parse(ipaddr),prt ));
			mystream = new NetworkStream(mysocket,true);
		}
		public void StartProcessor(){
			thread = new Thread(new ThreadStart(this.ThreadProc));
			state= RemoteProcessorStates.BLOCKED;
			thread.Start();
		}
		public void StopProcessor(){
			state=RemoteProcessorStates.STOP;
			thread.Interrupt();
		}
		public void Disconnect(){
			if(!Object.ReferenceEquals(mystream,null))
				mystream.Close();
			else
				mystream = null;
			mysocket = null;
		}
		public void Proccess()
		{
			//this.localprocessor.currentcode = localprocessor.currentcode;
			//RemoteProcessorStates tmp = state;
			state=RemoteProcessorStates.RUNNING;
			//while(thread.ThreadState == ThreadState.Suspended)
			//{
				//try
				//{
					//Console.WriteLine(thread.ThreadState.ToString());
					thread.Interrupt();
				//}
				//catch(ThreadStateException e)
				//{
					//Console.WriteLine("Not User Suspended -- " + thread.ThreadState.ToString());
				//}
			//}
		}
		public void ThreadProc()
		{

			while(state != RemoteProcessorStates.STOP)
			{
				try
				{
					switch(state)
					{
				
						case RemoteProcessorStates.WAITING:
							try
							{
								RecievePacket();
							}
							catch(Exception exc)
							{
								//System.Windows.Forms.MessageBox.Show(exc.Message);
								localprocessor.raiseerror("One or more remote processors are failed.Error Detail - " + exc.Message );
								state=RemoteProcessorStates.STOP;
							}							
							break;

						case RemoteProcessorStates.BLOCKED:
							//Console.WriteLine("Blocking - {0}",joinflag);
							if(joinflag)
							{
								joinflag = false;
								joinedthread.Resume();
								joinedthread = null;
							}
							//Console.WriteLine("Blocking - Endd {0}",joinflag);
							if(state==RemoteProcessorStates.BLOCKED)Thread.Sleep(Timeout.Infinite);
							break;
				
						case RemoteProcessorStates.RUNNING:
							try
							{
								ProcessRequest();
							}
							catch(Exception exc)
							{
								//System.Windows.Forms.MessageBox.Show(exc.Message);
								localprocessor.raiseerror(exc.Message);
								state=RemoteProcessorStates.STOP;
							}							
							break;
						
					
					}
				}
				catch(ThreadInterruptedException e){
					
				}
			}
			if(joinflag)
			{
				joinflag = false;
				joinedthread.Resume();
				joinedthread = null;
			}
		}	
		public void Join(){
			if(state==RemoteProcessorStates.WAITING || state==RemoteProcessorStates.RUNNING)
			{
				if(joinflag==false)
				{
					joinedthread = Thread.CurrentThread;
					joinflag=true;
					Thread.CurrentThread.Suspend();
				}
			}
		}
		private void ProcessRequest(){

			int tmp =((mthread.endthread-mthread.inspointer)+1);
			packet = new byte[100+tmp*2];
			
			packet[4] = (byte)RemoteProtocolMsgs.START;
			
			Buffer.BlockCopy(BitConverter.GetBytes(tmp),0,packet,5,4);
			Buffer.BlockCopy(localprocessor.currentcode.instructions,mthread.inspointer*2,packet,9,tmp*2);
			int offset = 9+tmp*2;
			FillMemoryData(ref offset);
			Buffer.BlockCopy(BitConverter.GetBytes(offset-4),0,packet,0,4);
			mystream.Write(packet,0,offset);

			state = RemoteProcessorStates.WAITING;
	//		RecievePacket();
		}
		/*private void Trim(uint length){
			byte[] tmp = new byte[length];
			Buffer.BlockCopy(packet,0,tmp,0,length);
			packet = tmp;
		}*/
		private void RecievePacket(){
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
		private void ProcessPacket(){
			//int pointer=0;
			switch(packet[0])
			{
				case (byte)RemoteProtocolMsgs.GETMATRIX:
                    int loc = BitConverter.ToInt32(packet,1);
					int blen = Buffer.ByteLength(localprocessor.currentcode.matrixmemory[loc].m_MatArr);
					packet = new byte[17+blen];
					
					packet[4] = (byte)RemoteProtocolMsgs.RECIEVEMATRIX;
					Buffer.BlockCopy(BitConverter.GetBytes(packet.Length-4),0,packet,0,4);
					Buffer.BlockCopy(BitConverter.GetBytes(loc),0,packet,5,4);
					Buffer.BlockCopy(BitConverter.GetBytes(localprocessor.currentcode.matrixmemory[loc].Rows),0,packet,9,4);
					Buffer.BlockCopy(BitConverter.GetBytes(localprocessor.currentcode.matrixmemory[loc].Columns),0,packet,13,4);
					Buffer.BlockCopy(localprocessor.currentcode.matrixmemory[loc].m_MatArr,0,packet,17,blen);
					mystream.Write(packet,0,packet.Length);
					break;
				case (byte)RemoteProtocolMsgs.END:
					ProcessResponse();
					state = RemoteProcessorStates.BLOCKED;
					break;
				case (byte)RemoteProtocolMsgs.ERROR:
					ProcessError();
					state = RemoteProcessorStates.BLOCKED;
					break;

			}							
		}
		private void ProcessError(){
			string message = System.Text.ASCIIEncoding.ASCII.GetString(packet,1,packet.Length-1);
			//System.Windows.Forms.MessageBox.Show(message);
			throw new Exception(message);
		}
		private void ProcessResponse(){
			
			int mbytes = BitConverter.ToInt32(packet,1)+4;
			
			int pointer = 5;
			
			while(pointer<=mbytes){
				
				ushort addr = (ushort)BitConverter.ToInt16(packet,pointer);
				pointer+=2;
				localprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.DATA] = 
					BitConverter.ToDouble(packet,pointer);
				pointer+=8;
				localprocessor.currentcode.memory[addr,(int)MEMCONSTANTS.TYPE] = packet[pointer];
				pointer++;
			}

			while(pointer<packet.Length){
				
				int matref,rw,cl,blen;
				if(packet[pointer]==(byte)MatrixState.MODIFIED 
					|| packet[pointer]==(byte)MatrixState.OVERWITTEN)
				{
					pointer++;
					matref = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
					rw  = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
					cl  = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
					blen = rw*cl*8;
					
					localprocessor.currentcode.matrixmemory
						[matref] = new Matrix(rw,cl);
					Buffer.BlockCopy(packet,pointer,
						localprocessor.currentcode.matrixmemory[matref].m_MatArr,0,blen);
					
					pointer+=blen;
				}
				else if(packet[pointer]==(byte)MatrixState.NEWMATRIX)
				{
					pointer++;
					matref = BitConverter.ToInt16(packet,pointer);
					pointer+=2;

					rw  = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
					cl  = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
					blen = rw*cl*8;

					int tmp = localprocessor.PlaceMatrix(new Matrix(rw,cl));
					Buffer.BlockCopy(packet,pointer,
						localprocessor.currentcode.matrixmemory[tmp].m_MatArr,0,blen);
					localprocessor.currentcode.memory[matref,(int)MEMCONSTANTS.DATA]=
						tmp;
					pointer+=blen;
				}
				else if(packet[pointer]==(byte)MatrixState.REMOVED){
					pointer++;
					matref = BitConverter.ToInt32(packet,pointer);
					pointer+=4;
                    localprocessor.currentcode.matrixmemory[matref] = null;				
				}
			}
		}
		/*private void FillMemoryData(ref int offset){
			
			bool[] memflag = new bool[localprocessor.currentcode.memory.Length];
	
			int pointer = mthread.inspointer;
			ushort maxref=0;
			int packetpointer=offset + 6;
			while(pointer<=mthread.endthread){
				ushort tempvar;
				switch(localprocessor.currentcode.instructions[pointer]){
					
					case (ushort)ASMPROCESSOR_OPERATIONS.STORE:
					case (ushort)ASMPROCESSOR_OPERATIONS.GETMEMORY:
						pointer++;
						tempvar = localprocessor.currentcode.instructions[pointer];
						if(memflag[tempvar]==false)
						{
							if(packetpointer+11>=packet.Length)IncreaseCapacity(55,packet);
							if(maxref<tempvar)maxref=tempvar;
							Buffer.BlockCopy(BitConverter.GetBytes(tempvar),0,packet,packetpointer,2);
							packetpointer+=2;
							Buffer.BlockCopy(BitConverter.GetBytes(localprocessor.currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA]),0,packet,packetpointer,8);
							packetpointer+=8;
							packet[packetpointer] = (byte)localprocessor.currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE];
							packetpointer++;
							memflag[tempvar]=true;
						}
						break;
				}
				pointer++;
			}
			Buffer.BlockCopy(BitConverter.GetBytes(maxref),0,packet,offset+4,2);
			Buffer.BlockCopy(BitConverter.GetBytes(packetpointer-(offset+4)),0,packet,offset,4);
			offset=packetpointer;
		}*/
		private void FillMemoryData(ref int offset)
		{
			
			int mbytes = (memend-memstart)*11+2;
			
			Buffer.BlockCopy(BitConverter.GetBytes(mbytes),0,packet,offset,4);
			offset=offset+4;

			if(offset+mbytes>=packet.Length)IncreaseCapacity(mbytes,ref packet);

			Buffer.BlockCopy(BitConverter.GetBytes(localprocessor.currentcode.instructions[memstart]),0,packet,offset,2);
			offset=offset+2;
			memstart++;
			while(memstart<=memend)
			{
				Buffer.BlockCopy(BitConverter.GetBytes(
						localprocessor.currentcode.instructions[memstart]),0,packet,offset,2);
				offset+=2;
				Buffer.BlockCopy(BitConverter.GetBytes(
					localprocessor.currentcode.memory[localprocessor.currentcode.instructions[memstart],
							(int)MEMCONSTANTS.DATA]),0,packet,offset,8);
				offset+=8;
				packet[offset] = (byte)localprocessor.currentcode.memory[
							localprocessor.currentcode.instructions[memstart],(int)MEMCONSTANTS.TYPE];
				offset++;
				memstart++;
			}
			
		}
		public void SendNumericConstants(){
			packet = new byte[5+Buffer.ByteLength(localprocessor.currentcode.numconstants)];
			packet[4] = (byte)RemoteProtocolMsgs.NUMCOS;
			Buffer.BlockCopy(localprocessor.currentcode.numconstants,0,packet,5,Buffer.ByteLength(localprocessor.currentcode.numconstants));
			Buffer.BlockCopy(BitConverter.GetBytes(packet.Length-4),0,packet,0,4);
			mystream.Write(packet,0,packet.Length);
	//		RecievePacket();
		}
		public void SendPacket(byte[] packet)
		{
			mystream.Write(packet,0,packet.Length);
	//		RecievePacket();
		}
		private void IncreaseCapacity(int amnt,ref byte[] packet){
			byte[] tmp = new byte[packet.Length +amnt];
			Buffer.BlockCopy(packet,0,tmp,0,packet.Length);
			packet = tmp;
		}
	}
}
