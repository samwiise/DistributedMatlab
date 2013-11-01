using System;

namespace ASMDM
{
	
	public interface IExternalDevicePort
	{
		void Initialize(int mport);
		int WaitForDevice();
		ASMMath.Matrix GetMatrix();
		void PutMatrix(ASMMath.Matrix matrix);
		void ClosePort();	

		bool IsConnected{
			get;
		}

	}
}
