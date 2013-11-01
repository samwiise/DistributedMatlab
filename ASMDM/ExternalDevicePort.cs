using System;
using System.Collections;
using ASMMath;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ASMDM
{
	public class ExternalDevicePort:IExternalDevicePort
	{
		public delegate void Echo(string message);

		private enum ThreadStates{
			LISTEN,RECIEVE,STOP
		}

		public string devicename;

		private ExternalDevicePort.Echo EchoMess;

		private Socket msocket;
		private Socket listener;
		private NetworkStream mstream;

		private Thread mthread;
		private Queue rmqueue;
		private bool isconnected;
		private ExternalDevicePort.ThreadStates state; 
	
		private Thread blockedthread;
		private bool blockthreadflag;

		public ExternalDevicePort(string devicename,Echo echomess)
		{ 
			this.devicename = devicename;
			this.EchoMess = echomess;
		}
		public void Initialize(int mport){
			
			rmqueue = Queue.Synchronized(new Queue(10));
			mthread =  new Thread(new ThreadStart(this.ThreadProc));
			state=ThreadStates.LISTEN;
			isconnected=false;
			blockthreadflag = false;

			listener=new Socket(AddressFamily.InterNetwork,
				SocketType.Stream,ProtocolType.Tcp);
			listener.Bind(new IPEndPoint(IPAddress.Any,mport));
			
			EchoMessage("listening on port " + 
				((IPEndPoint)listener.LocalEndPoint).Port.ToString()
				+ " for " + devicename);
			
			listener.Listen(1);            
			mthread.Start();
		}

		public void ThreadProc(){
			
			while(state!=ThreadStates.STOP)
			{
				switch(state)
				{
							
					case ThreadStates.LISTEN:
						try
						{
							Listen();						
						}
						catch(Exception e){
							state=ThreadStates.STOP;
						}
						break;

					case ThreadStates.RECIEVE:
						try
						{
							RecievePacket();
						}
						catch(Exception e){
							if(state!=ThreadStates.STOP)
								state=ThreadStates.LISTEN;						
						}
						break;
				
				}
			}
			
			if(blockthreadflag)
				blockedthread.Resume();

			blockthreadflag=false;
			blockedthread=null;
			
			FreeSocket();
			rmqueue=null;

			listener = null;
		}
		private void Listen(){
			FreeSocket();
			rmqueue.Clear();
			
			msocket = listener.Accept();
			
			EchoMessage(devicename + " is connected.");
			mstream = new NetworkStream(msocket,true);
			isconnected =true;
			state = ThreadStates.RECIEVE;

			if(blockthreadflag){
				blockthreadflag=false;
				blockedthread.Resume();
				blockedthread=null;
			}
		}
		
		private void EchoMessage(string message){
			if(EchoMess !=null)
				EchoMess(message);
		}
		private void RecievePacket()
		{
			byte[] packet = new byte[4];
			int tmp = mstream.Read(packet,0,4);
			if(tmp==0)throw new Exception("Connection has been closed from the remote side.");
			
			
			int packetsize = BitConverter.ToInt32(packet,0);
			packet = new byte[packetsize];
			int bytesread=0;
			while(bytesread<packetsize)
			{
				tmp = mstream.Read(packet,bytesread,packetsize-bytesread);
				if(tmp==0)throw new Exception("Connection has been closed from the remote side.");
				bytesread +=tmp;
			}
			if(rmqueue.Count<10)
				ProcessPacket(packet);
		}

		private void ProcessPacket(byte[] packet){
			try
			{			
				int rw = BitConverter.ToInt32(packet,0);
				int cl = BitConverter.ToInt32(packet,4);
						
				Matrix temp = new Matrix(rw,cl);
				Buffer.BlockCopy(packet,8,temp.m_MatArr,0,rw*cl*8);
			
				rmqueue.Enqueue(temp);
			}
			catch (Exception e){
			}
		}
		private void FreeSocket()
		{
			if(mstream!=null)
			{
				EchoMessage(devicename + " is disconnected.");
				mstream.Close();
				mstream = null;
			}
			msocket = null;
			isconnected=false;
		}
		
		public int WaitForDevice(){
			if(state==ThreadStates.LISTEN && blockthreadflag==false)
			{
				blockthreadflag=true;
				blockedthread = Thread.CurrentThread;
				Thread.CurrentThread.Suspend();
			}

			if(isconnected){
				return 1;
			}else
				return 0;
		}

		public Matrix GetMatrix(){
			if(rmqueue.Count==0)
				return null;
			else
				return (Matrix)rmqueue.Dequeue();
		}
		public void PutMatrix(Matrix matrix){
			int blen = Buffer.ByteLength(matrix.m_MatArr);
			byte[] packet = new byte[12+blen];
					
			Buffer.BlockCopy(BitConverter.GetBytes(packet.Length-4),0,packet,0,4);
			Buffer.BlockCopy(BitConverter.GetBytes(matrix.Rows),0,packet,4,4);
			Buffer.BlockCopy(BitConverter.GetBytes(matrix.Columns),0,packet,8,4);
			Buffer.BlockCopy(matrix.m_MatArr,0,packet,12,blen);
			mstream.Write(packet,0,packet.Length);			
		}
		public void ClosePort(){
			state=ThreadStates.STOP;
			listener.Close();
			if(msocket!=null)
				msocket.Close();
		}

		public bool IsConnected
		{
			get{
				if(isconnected==true && state==ThreadStates.RECIEVE)
					return true;
				else 
					return false;
			}
		}
	}
}
