using System;
using ASMMath;

namespace ASMDM
{

	public enum ASMPROCESSOR_OPERATIONS
	{
		GETCONSTANT,MULTIPLY,SUBTRACT,DIVIDE,ADD,EXPONENT,MOD,STORE,GETMEMORY,PRINT,NEWMATRIX,STOREMATRIX,GETMATRIXELEMENT,PUSHSTACK,JMP,
		NEGATIVE,ISEQUAL,ISGREATERTHAN,ISLESSTHAN,ISNOTEQUAL,LOGICALAND,LOGICALOR,LOGICALNOT,STOREEXTRA,ISGREATERTHANEQUAL,ISLESSTHANEQUAL,
		JMPCMP,JMPCMP2,STARTMATRIX,TRANSPOSE,ADJOINT,SIN,COS,TAN,ARCSIN,ARCCOS,ARCTAN,INPUTNUMBER,GETMATRIXFROMDEVICE,SETMATRIXTODEVICE,WAITFORDEVICE,
		NEWIDENTITYMATRIX,PARALLELSTART,ISMATRIX,GETROWS,GETCOLUMNS,SQRT,GETPRIMES,GETPRIMESUPTO,ROWDEFINEDBYVALUES,ROWDEFINEDBYRANGE,
		PARALLELEND,STATEMENTSTART,STATEMENTEND,CREATETHREAD
	}
	public enum MEMCONSTANTS
	{
		DATA,TYPE
	}
	public enum MEMTYPECONSTANTS
	{
		VALUE,REFERENCE,TEMPREFERENCE,LITERAL
	}
	public enum RemoteProtocolMsgs
	{
		START,NUMCOS,END,ERROR,GETMATRIX,RECIEVEMATRIX,LITERALS
	}
	public enum MatrixState
	{
		UNUSED,NOTAVAILABLE,AVAILABLE,REMOVED,NEWMATRIX,MODIFIED,OVERWITTEN
	}

	public struct ProgramCode
	{
		public ushort[] instructions;
		public double[] numconstants;
		public string[] literals;
		public Matrix[] matrixmemory;
		public double[,] memory;
		public ISystemBus bus;
		public MatrixState[] matrixstate;
	}
	public struct ProgramThread
	{
		public double [,] pstack;
		public int inspointer;
		public int stackpointer;
		public int endthread;
		public bool cmpflag;
	}

	
	public delegate void ASMProcessorOperation(ref ProgramThread currentthread);
	public delegate void RaiseError (string errormsg);


	public interface IInputTerminal
	{
		double Input(string message);
	}
	public interface IOutputTerminal
	{
		
		void Output(string data);
	}
	public interface ISystemBus
	{
		IInputTerminal In
		{
			get;
		}

		IExternalDevicePort[] ExternalDevices
		{
			get;
		}
		IOutputTerminal Out
		{
			get;
		}
	}
}
