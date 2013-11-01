using System;
using ASMDM;
using ASMMath;
using System.Threading;
using System.Collections;


namespace RemoteMachine
{
			
	public delegate void GetMatrixFromFar(int loc);

	public class ASMProcessorRS
	{
		
		//public ProgramThread currentthread;
		public ProgramCode currentcode;
		
		public RaiseError raiseerror;
		private GetMatrixFromFar GetMatrixIntoMemory;
		private ASMProcessorOperation[] ASMOperations = new ASMProcessorOperation[49];
		
		public ASMProcessorRS(GetMatrixFromFar dgetmatrix)
		{
			GetMatrixIntoMemory = dgetmatrix;

			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETCONSTANT] = new ASMProcessorOperation(this.ASM_GetConstant);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETMEMORY] = new ASMProcessorOperation(this.ASM_GetMemory);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.STORE] = new ASMProcessorOperation(this.ASM_Store);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ADD] = new ASMProcessorOperation(this.ASM_Add);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.PRINT] = new ASMProcessorOperation(this.ASM_Print);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.DIVIDE] = new ASMProcessorOperation(this.ASM_Divide);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.MULTIPLY] = new ASMProcessorOperation(this.ASM_Multiply);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.SUBTRACT] = new ASMProcessorOperation(this.ASM_Subtract);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.EXPONENT] = new ASMProcessorOperation(this.ASM_Exponent);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.MOD] = new ASMProcessorOperation(this.ASM_Mod);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.PUSHSTACK] = new ASMProcessorOperation(this.ASM_PushStack);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.NEWMATRIX] = new ASMProcessorOperation(this.ASM_NewMatrix);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.STOREMATRIX] = new ASMProcessorOperation(this.ASM_StoreMatrix);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETMATRIXELEMENT] = new ASMProcessorOperation(this.ASM_GetMatrixElement);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.NEGATIVE] = new ASMProcessorOperation(this.ASM_Negative);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISEQUAL] = new ASMProcessorOperation(this.ASM_IsEqual);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISNOTEQUAL] = new ASMProcessorOperation(this.ASM_IsNotEqual);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISGREATERTHAN] = new ASMProcessorOperation(this.ASM_IsGreaterThan);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISLESSTHAN] = new ASMProcessorOperation(this.ASM_IsLessThan);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISGREATERTHANEQUAL] = new ASMProcessorOperation(this.ASM_IsGreaterThanEqual);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISLESSTHANEQUAL] = new ASMProcessorOperation(this.ASM_IsLessThanEqual);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.LOGICALAND] = new ASMProcessorOperation(this.ASM_LogicalAnd);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.LOGICALOR] = new ASMProcessorOperation(this.ASM_LogicalOR);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.LOGICALNOT] = new ASMProcessorOperation(this.ASM_LogicalNot);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.STOREEXTRA] = new ASMProcessorOperation(this.ASM_StoreExtra);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.JMP] = new ASMProcessorOperation(this.ASM_Jmp);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.JMPCMP] = new ASMProcessorOperation(this.ASM_JmpCmp);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.JMPCMP2] = new ASMProcessorOperation(this.ASM_JmpCmp2);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.STARTMATRIX] = new ASMProcessorOperation(this.ASM_StartMatrix);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.TRANSPOSE] = new ASMProcessorOperation(this.ASM_Transpose);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ADJOINT] = new ASMProcessorOperation(this.ASM_Adjoint);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.COS] = new ASMProcessorOperation(this.ASM_Cos);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.SIN] = new ASMProcessorOperation(this.ASM_Sin);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.TAN] = new ASMProcessorOperation(this.ASM_Tan);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ARCCOS] = new ASMProcessorOperation(this.ASM_ArcCos);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ARCSIN] = new ASMProcessorOperation(this.ASM_ArcSin);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ARCTAN] = new ASMProcessorOperation(this.ASM_ArcTan);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.NEWIDENTITYMATRIX] = new ASMProcessorOperation(this.ASM_NewIdentityMatrix);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.PARALLELSTART] = new ASMProcessorOperation(this.ASM_ParallelStart);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.INPUTNUMBER] = new ASMProcessorOperation(this.ASM_InputNumber);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.WAITFORDEVICE] = new ASMProcessorOperation(this.ASM_WaitForDevice);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.SETMATRIXTODEVICE] = new ASMProcessorOperation(this.ASM_SetMatrixToDevice);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETMATRIXFROMDEVICE] = new ASMProcessorOperation(this.ASM_GetMatrixFromDevice);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.ISMATRIX] = new ASMProcessorOperation(this.ASM_IsMatrix);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETROWS] = new ASMProcessorOperation(this.ASM_GetRows);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETCOLUMNS] = new ASMProcessorOperation(this.ASM_GetColumns);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETPRIMES] = new ASMProcessorOperation(this.ASM_GetPrimes);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.GETPRIMESUPTO] = new ASMProcessorOperation(this.ASM_GetPrimesUpto);
			ASMOperations[(int)ASMPROCESSOR_OPERATIONS.SQRT] = new ASMProcessorOperation(this.ASM_Sqrt);
		}
		
		/*private void PushStack(ref ProgramThread currentthread,double pvalue,MEMTYPECONSTANTS ptype){
			//lock(this)
			//{
				currentthread.stackpointer++;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] = pvalue;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] = (double)ptype;
			//}
		}*/
		/*private void PopStack(ref ProgramThread currentthread)
		{
			//lock(this)
			//{
				currentthread.stackpointer--;
			//}
		}*/
		private int PlaceMatrix(Matrix matrix)
		{
			lock(this)
			{
				for(int a=0;a<currentcode.matrixstate.Length;a++)
					if(currentcode.matrixstate[a]==MatrixState.UNUSED)
					{
						currentcode.matrixmemory[a] = matrix;
						currentcode.matrixstate[a] = MatrixState.NEWMATRIX;
						return a;
					}
					else if(currentcode.matrixstate[a]==MatrixState.REMOVED){
						currentcode.matrixmemory[a] = matrix;
						currentcode.matrixstate[a] = MatrixState.OVERWITTEN;
						return a;										
					}
			}
			throw new Exception("Not enough Matrix Memory.");
		}
		private void FreeMatrixMemory(int loc){
			currentcode.matrixmemory[loc]= null;
			if(currentcode.matrixstate[loc]==MatrixState.OVERWITTEN || 
					currentcode.matrixstate[loc]==MatrixState.AVAILABLE ||
						currentcode.matrixstate[loc]==MatrixState.MODIFIED ||
							currentcode.matrixstate[loc]==MatrixState.NOTAVAILABLE)
				currentcode.matrixstate[loc] = MatrixState.REMOVED;
			else
				currentcode.matrixstate[loc] = MatrixState.UNUSED;
		}
		private void MatrixStateModified(int loc){
			if(currentcode.matrixstate[loc]==MatrixState.AVAILABLE)
				currentcode.matrixstate[loc] = MatrixState.MODIFIED;
		}
		private void GetMatrix(int loc){
			if(currentcode.matrixstate[loc]==MatrixState.NOTAVAILABLE)
				GetMatrixIntoMemory(loc);
		}
		public void Process(ref ProgramThread currentthread)
		{
			while(currentthread.inspointer<=currentthread.endthread)
			{
				ASMOperations[currentcode.instructions[currentthread.inspointer]](ref  currentthread);
				currentthread.inspointer++;
			}
		}
		private void ASM_PushStack(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			ushort tempvar = currentcode.instructions[currentthread.inspointer];
			currentthread.inspointer++;
			ushort tempvar2 = currentcode.instructions[currentthread.inspointer];
			//PushStack(ref currentthread,tempvar,(MEMTYPECONSTANTS)tempvar2);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] = tempvar;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] = tempvar2;
			//PushStack End
		}
		private void ASM_Jmp(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			currentthread.inspointer += ((int)currentcode.numconstants[currentcode.instructions [currentthread.inspointer]]);
		}
		private void ASM_JmpCmp(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			if(!currentthread.cmpflag)currentthread.inspointer += ((int)currentcode.numconstants[currentcode.instructions [currentthread.inspointer]]);
		}
		private void ASM_JmpCmp2(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			if(currentthread.cmpflag)currentthread.inspointer += ((int)currentcode.numconstants[currentcode.instructions [currentthread.inspointer]]);
		}
		private void ASM_GetConstant(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			//PushStack(ref currentthread,currentcode.numconstants [
			//	currentcode.instructions [currentthread.inspointer]
			//	],MEMTYPECONSTANTS.VALUE);				
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] = 
				currentcode.numconstants [currentcode.instructions 
				[currentthread.inspointer]];
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] = (double)MEMTYPECONSTANTS.VALUE;
			//PushStack End
		}
		private void ASM_GetMemory(ref ProgramThread currentthread)
		{
			ushort tempvar;
			currentthread.inspointer++;
			tempvar = currentcode.instructions [currentthread.inspointer];
			//PushStack(ref currentthread,currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA],
			//	(MEMTYPECONSTANTS)currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE]);

			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] = currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA];
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] = currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE];
			//PushStack End
		}
		private void ASM_Add(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = tempvar1+tempvar2;
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				Matrix temp = currentcode.matrixmemory[(int)tempvar2];
				temp = temp + tempvar1;
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp + tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified((int)tempvar1);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				GetMatrix((int)tempvar1);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1+temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					MatrixStateModified((int)tempvar1);
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Multiply(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = tempvar1*tempvar2;
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				Matrix temp = currentcode.matrixmemory[(int)tempvar2];
				temp = temp * tempvar1;
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp * tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified((int)tempvar2);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				GetMatrix((int)tempvar1);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1*temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					MatrixStateModified((int)tempvar1);
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);;
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Subtract(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = tempvar1-tempvar2;
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp - tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					MatrixStateModified((int)tempvar1);
					currentcode.matrixmemory[(int)tempvar1] = temp;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2].InvSub(tempvar1);
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
				}
				else
				{
					Matrix temp = currentcode.matrixmemory[(int)tempvar2];
					temp = temp.ReturnInvSub(tempvar1);
					tempvar1 = PlaceMatrix(temp);
				}
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				GetMatrix((int)tempvar2);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1-temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					MatrixStateModified((int)tempvar1);
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					MatrixStateModified((int)tempvar2);
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Divide(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = tempvar1/tempvar2;
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp / tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified((int)tempvar1);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2].InvDiv(tempvar1);
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
				}
				else
				{
					Matrix temp = currentcode.matrixmemory[(int)tempvar2];
					temp = temp.ReturnInvDiv(tempvar1);
					tempvar1 = PlaceMatrix(temp);
				}
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				GetMatrix((int)tempvar2);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1/temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					MatrixStateModified((int)tempvar1);
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Exponent(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = Math.Pow(tempvar1,tempvar2);
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp ^ tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified( (int)tempvar1);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2].InvExp(tempvar1);
					MatrixStateModified( (int)tempvar2);
					tempvar1 = tempvar2;
				}
				else
				{
					Matrix temp = currentcode.matrixmemory[(int)tempvar2];
					temp = temp.ReturnInvExp(tempvar1);
					tempvar1 = PlaceMatrix(temp);
				}
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				GetMatrix( (int)tempvar2);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1^temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					MatrixStateModified( (int)tempvar1);
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					MatrixStateModified((int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Mod(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = tempvar1 % tempvar2;
			else if(tempvar2type==MEMTYPECONSTANTS.VALUE && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp % tempvar2;
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified( (int)tempvar1);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else if(tempvar1type==MEMTYPECONSTANTS.VALUE && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar2);
				if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2].InvMod(tempvar1);
					MatrixStateModified( (int)tempvar2);
					tempvar1 = tempvar2;
				}
				else
				{
					Matrix temp = currentcode.matrixmemory[(int)tempvar2];
					temp = temp.ReturnInvMod(tempvar1);
					tempvar1 = PlaceMatrix(temp);
				}
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if((tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);
				GetMatrix((int)tempvar2);
				Matrix temp1 = currentcode.matrixmemory[(int)tempvar1];
				Matrix temp2 = currentcode.matrixmemory[(int)tempvar2];
				temp1 = temp1%temp2;

				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp1;
					MatrixStateModified( (int)tempvar1);
					if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
				}
				else if(tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar2] = temp1;
					MatrixStateModified( (int)tempvar2);
					tempvar1 = tempvar2;
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
				else
				{
					tempvar1 = PlaceMatrix(temp1);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Negative(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type==MEMTYPECONSTANTS.VALUE)
				tempvar1 = -(tempvar1);
			else if(tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
			{
				GetMatrix((int)tempvar1);
				Matrix temp = currentcode.matrixmemory[(int)tempvar1];
				temp = temp.ReturnNegative();
				if(tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					currentcode.matrixmemory[(int)tempvar1] = temp;
					MatrixStateModified((int)tempvar1);
				}
				else
				{
					tempvar1 = PlaceMatrix(temp);
					tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
				}
			}
			else
				throw new Exception("Type mismatch");
			
			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Store(ref ProgramThread currentthread)
		{
			ushort tempvar;
			int rmm=-2;
			currentthread.inspointer++;
			if((MEMTYPECONSTANTS)currentcode.memory[currentcode.instructions [
				currentthread.inspointer],(int)MEMCONSTANTS.TYPE]== 
				MEMTYPECONSTANTS.REFERENCE ||(MEMTYPECONSTANTS)currentcode.memory[
				currentcode.instructions[currentthread.inspointer],
				(int)MEMCONSTANTS.TYPE]==MEMTYPECONSTANTS.TEMPREFERENCE)
				rmm =(int)currentcode.memory[currentcode.instructions
					[currentthread.inspointer],(int)MEMCONSTANTS.DATA];
				
			tempvar =(ushort)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];
			if(tempvar==(ushort)MEMTYPECONSTANTS.VALUE)
			{
				tempvar = currentcode.instructions [currentthread.inspointer];
				currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA] = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];
				currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE] = (double)MEMTYPECONSTANTS.VALUE;
			}
			else if(tempvar==(ushort)MEMTYPECONSTANTS.REFERENCE)
			{
				tempvar = currentcode.instructions [currentthread.inspointer];
				GetMatrix((int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA]);
				Matrix temp = currentcode.matrixmemory[(int)currentthread.pstack
					[currentthread.stackpointer,(int)MEMCONSTANTS.DATA]].ReturnClone();
				currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA]= PlaceMatrix(temp);
				currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE] = (double)MEMTYPECONSTANTS.REFERENCE;
			}
			else if(tempvar==(ushort)MEMTYPECONSTANTS.TEMPREFERENCE)
			{
				tempvar = currentcode.instructions [currentthread.inspointer];
				currentcode.memory[tempvar,(int)MEMCONSTANTS.DATA]= currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];
				currentcode.memory[tempvar,(int)MEMCONSTANTS.TYPE] = (double)MEMTYPECONSTANTS.REFERENCE;
			}
			currentthread.stackpointer--;		
			if(rmm>=0)
				FreeMatrixMemory(rmm);
		}
		private void ASM_Print(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;
			string output="";

			currentthread.inspointer++;
			ushort argcount = currentcode.instructions[
				currentthread.inspointer];

			for(ushort tmp=1;tmp<=argcount;tmp++)
			{
				tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
				tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
				currentthread.stackpointer--;

				if(tempvar1type == MEMTYPECONSTANTS.VALUE)
					output += tempvar1.ToString()+"     ";
				else if(tempvar1type == MEMTYPECONSTANTS.REFERENCE || tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					GetMatrix((int)tempvar1);
					output += currentcode.matrixmemory[(int)tempvar1].Display()+"     ";
					if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar1);
				}
				else if(tempvar1type == MEMTYPECONSTANTS.LITERAL)
					output += currentcode.literals[(int)tempvar1]+"     ";
				else
					throw new Exception("Type mismatch");
			}
			currentcode.bus.Out.Output(output);
			currentcode.bus.Out.Output("\r\n");
		}
		
		private void ASM_InputNumber(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;
			string output="";

			currentthread.inspointer++;
			ushort argcount = currentcode.instructions[
				currentthread.inspointer];

			for(ushort tmp=1;tmp<=argcount;tmp++)
			{
				tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
				tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
				currentthread.stackpointer--;

				if(tempvar1type == MEMTYPECONSTANTS.VALUE)
					output += tempvar1.ToString();
				else if(tempvar1type == MEMTYPECONSTANTS.REFERENCE || tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)
				{
					GetMatrix((int)tempvar1);
					output += currentcode.matrixmemory[(int)tempvar1].Display();
					if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)currentcode.matrixmemory[(int)tempvar1]= null;
				}
				else if(tempvar1type == MEMTYPECONSTANTS.LITERAL)
					output += currentcode.literals[(int)tempvar1];
				else
					throw new Exception("Type mismatch");
			}
			lock(this)
			{
				//currentcode.bus.Out.Output(output);
			
				//PushStack Start
				currentthread.stackpointer++;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =currentcode.bus.In.Input(output);
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				//PushStack End
			}
		}
		private void ASM_NewMatrix(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type != MEMTYPECONSTANTS.VALUE || tempvar2type != MEMTYPECONSTANTS.VALUE)throw new Exception("Type mismatch");

			tempvar1 = PlaceMatrix(new Matrix((int)tempvar1,(int)tempvar2));
			tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_StartMatrix(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			int rws = currentcode.instructions[currentthread.inspointer];
			currentthread.inspointer++;
			int cols = (int)currentcode.numconstants[currentcode.instructions[currentthread.inspointer]];
			Matrix temp =  new Matrix(rws,cols);

			for(int rw=0;rw<rws;rw++)
			{
				currentthread.inspointer++;
				
				if((ASMPROCESSOR_OPERATIONS)currentcode.instructions[currentthread.inspointer]==ASMPROCESSOR_OPERATIONS.ROWDEFINEDBYVALUES)
					for(int cl=0;cl<cols;cl++)
					{
						currentthread.inspointer++;
						temp.m_MatArr[rw,cl] = currentcode.numconstants[currentcode.instructions[currentthread.inspointer]];
					}
				else
				{
					double st,inc;
					currentthread.inspointer++;
					st = currentcode.numconstants[currentcode.instructions[currentthread.inspointer]];
					currentthread.inspointer++;
					inc = currentcode.numconstants[currentcode.instructions[currentthread.inspointer]];
					double tmp=st;
					for(int cl=0;cl<cols;cl++)
					{
						temp.m_MatArr[rw,cl] = tmp;
						tmp=tmp+inc;
					}
				}
			}
			//PushStack(ref currentthread,PlaceMatrix(temp),MEMTYPECONSTANTS.TEMPREFERENCE);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =PlaceMatrix(temp);
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.TEMPREFERENCE;
			//PushStack End
		}
		private void ASM_StoreMatrix(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2,tempvar3,tempvar4;
			MEMTYPECONSTANTS tempvar1type,tempvar2type,tempvar3type,tempvar4type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar3 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar3type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar4 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar4type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type != MEMTYPECONSTANTS.REFERENCE )throw new Exception("Type mismatch");
			if(tempvar2type != MEMTYPECONSTANTS.VALUE )throw new Exception("Type mismatch");
			if(tempvar3type != MEMTYPECONSTANTS.VALUE )throw new Exception("Type mismatch");
			if(tempvar4type != MEMTYPECONSTANTS.VALUE )throw new Exception("Type mismatch");

			GetMatrix((int)tempvar1);
			currentcode.matrixmemory[(int)tempvar1].SetValue((int)tempvar2,(int)tempvar3,tempvar4);
			MatrixStateModified((int)tempvar1);
		}
		private void ASM_GetMatrixElement(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2,tempvar3;
			MEMTYPECONSTANTS tempvar1type,tempvar2type,tempvar3type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar3 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar3type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type != MEMTYPECONSTANTS.REFERENCE )throw new Exception("Type mismatch");
			if(tempvar2type != MEMTYPECONSTANTS.VALUE )throw new Exception("Type mismatch");
			if(tempvar3type != MEMTYPECONSTANTS.VALUE )throw new Exception("Type mismatch");

			//PushStack(ref currentthread,currentcode.matrixmemory[(int)tempvar1].GetValue((int)tempvar2,(int)tempvar3),MEMTYPECONSTANTS.VALUE);
			//PushStack Start
			currentthread.stackpointer++;
			GetMatrix((int)tempvar1);
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =currentcode.matrixmemory[(int)tempvar1].GetValue((int)tempvar2,(int)tempvar3);
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
			//PushStack End
			
		}
		private void ASM_IsEqual(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 == tempvar2);
			else if((tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);GetMatrix((int)tempvar2);
				currentthread.cmpflag = (currentcode.matrixmemory[(int)tempvar1] == currentcode.matrixmemory[(int)tempvar2]);

				if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar1);
				if(tempvar2type == MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
			}
			else
				throw new Exception("Type mismatch");
		}
		private void ASM_IsNotEqual(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 != tempvar2);
			else if((tempvar1type==MEMTYPECONSTANTS.REFERENCE || tempvar1type==MEMTYPECONSTANTS.TEMPREFERENCE) && (tempvar2type==MEMTYPECONSTANTS.REFERENCE || tempvar2type==MEMTYPECONSTANTS.TEMPREFERENCE))
			{
				GetMatrix((int)tempvar1);GetMatrix((int)tempvar2);
				currentthread.cmpflag = (currentcode.matrixmemory[(int)tempvar1] != currentcode.matrixmemory[(int)tempvar2]);

				if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar1);
				if(tempvar2type == MEMTYPECONSTANTS.TEMPREFERENCE)FreeMatrixMemory((int)tempvar2);
			}
			else
				throw new Exception("Type mismatch");
		}
		private void ASM_IsGreaterThan(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 > tempvar2);
			else
				throw new Exception("Type mismatch");
		}
		private void ASM_IsLessThan(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 < tempvar2);
			else
				throw new Exception("Type mismatch");
		}
		private void ASM_IsGreaterThanEqual(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 >= tempvar2);
			else
				throw new Exception("Type mismatch");
		}
		private void ASM_IsLessThanEqual(ref ProgramThread currentthread)
		{
			double tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;

			if(tempvar1type==MEMTYPECONSTANTS.VALUE && tempvar2type==MEMTYPECONSTANTS.VALUE)
				currentthread.cmpflag = (tempvar1 <= tempvar2);
			else
				throw new Exception("Type mismatch");
		}

		private void ASM_StoreExtra(ref ProgramThread currentthread)
		{
			if(currentthread.cmpflag)
			{
				//PushStack(ref currentthread,1,MEMTYPECONSTANTS.VALUE);
				//PushStack Start
				currentthread.stackpointer++;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =1;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				//PushStack End
			}
			else
			{
				//PushStack(ref currentthread,0,MEMTYPECONSTANTS.VALUE);
				//PushStack Start
				currentthread.stackpointer++;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =0;
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				//PushStack End
			}
		}
		private void ASM_LogicalAnd(ref ProgramThread currentthread)
		{
			bool extraflag;
			if(currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA]==1)
				extraflag = true;
			else
				extraflag = false;

			currentthread.stackpointer--;
			currentthread.cmpflag = (currentthread.cmpflag && extraflag);
		}
		private void ASM_LogicalOR(ref ProgramThread currentthread)
		{
			bool extraflag;
			if(currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA]==1)
				extraflag = true;
			else
				extraflag = false;

			currentthread.stackpointer--;
			currentthread.cmpflag = (currentthread.cmpflag || extraflag);
		}
		private void ASM_LogicalNot(ref ProgramThread currentthread)
		{
			currentthread.cmpflag = !(currentthread.cmpflag);
		}
		private void ASM_Transpose(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnTranspose());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				GetMatrix((int)tempvar1);
				currentcode.matrixmemory[(int)tempvar1].Transpose();
				MatrixStateModified((int)tempvar1);
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Adjoint(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnAdjoint());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				GetMatrix((int)tempvar1);
				currentcode.matrixmemory[(int)tempvar1]=currentcode.matrixmemory[(int)tempvar1].ReturnAdjoint();
				MatrixStateModified((int)tempvar1);
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Cos(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnCos());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].Cos();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Cos(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Sin(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnSin());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].Sin();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Sin(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Tan(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnTan());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].Tan();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Tan(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_ArcCos(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnArcCos());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].ArcCos();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Acos(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_ArcSin(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnArcSin());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].ArcSin();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Asin(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_ArcTan(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnArcTan());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				currentcode.matrixmemory[(int)tempvar1].ArcTan();
				MatrixStateModified((int)tempvar1);
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Atan(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_Sqrt(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE)
			{
				GetMatrix((int)tempvar1);
				tempvar1 = PlaceMatrix(currentcode.matrixmemory[(int)tempvar1].ReturnSquareRoot());
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE )
			{
				GetMatrix((int)tempvar1);
				currentcode.matrixmemory[(int)tempvar1].SquareRoot();
			}
			else if(tempvar1type == MEMTYPECONSTANTS.VALUE )
			{
				tempvar1 = Math.Sqrt(tempvar1);			
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_NewIdentityMatrix(ref ProgramThread currentthread)
		{
			double tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type == MEMTYPECONSTANTS.VALUE)
			{
				tempvar1 = PlaceMatrix(Matrix.NewIdentityMatrix((int)tempvar1));
				tempvar1type = MEMTYPECONSTANTS.TEMPREFERENCE;
			}
			else
				throw new Exception("Type mismatch");

			//PushStack(ref currentthread,tempvar1,tempvar1type);
			//PushStack Start
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =tempvar1;
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)tempvar1type;
			//PushStack End
		}
		private void ASM_WaitForDevice(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type != MEMTYPECONSTANTS.VALUE)
				throw new Exception("Type mismatch");
			
			//PushStack Start
			currentthread.stackpointer++;
			try
			{
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =
					currentcode.bus.ExternalDevices[tempvar1].WaitForDevice();
			}
			catch(Exception e)
			{
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =-1;
			}
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
			//PushStack End	
		}
		private void ASM_GetMatrixFromDevice(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type != MEMTYPECONSTANTS.VALUE)
				throw new Exception("Type mismatch");
			
			currentthread.stackpointer++;
			try
			{
				if(currentcode.bus.ExternalDevices[tempvar1].IsConnected)
				{
					Matrix tempmat = currentcode.bus.ExternalDevices
						[tempvar1].GetMatrix();
					if(Object.ReferenceEquals(tempmat,null))
					{
						currentthread.pstack[currentthread.stackpointer,
							(int)MEMCONSTANTS.DATA] =0;
						currentthread.pstack[currentthread.stackpointer,
							(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
					}
					else
					{
						currentthread.pstack[currentthread.stackpointer,
							(int)MEMCONSTANTS.DATA] = PlaceMatrix(tempmat);
						currentthread.pstack[currentthread.stackpointer,
							(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.TEMPREFERENCE;
					}
				}
				else
				{
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.DATA] =-2;
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				}

			}
			catch(Exception e)
			{
				currentthread.pstack[currentthread.stackpointer,
					(int)MEMCONSTANTS.DATA] =-1;
				currentthread.pstack[currentthread.stackpointer,
					(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
			}
		}
		private void ASM_SetMatrixToDevice(ref ProgramThread currentthread)
		{
			int tempvar1,tempvar2;
			MEMTYPECONSTANTS tempvar1type,tempvar2type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			tempvar2 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar2type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
			
			if(tempvar1type != MEMTYPECONSTANTS.VALUE || (tempvar2type !=
				MEMTYPECONSTANTS.REFERENCE && tempvar2type != 
				MEMTYPECONSTANTS.TEMPREFERENCE))
				throw new Exception("Type mismatch");
			
				
			currentthread.stackpointer++;
			try
			{
				if(currentcode.bus.ExternalDevices[tempvar1].IsConnected)
				{
					GetMatrix(tempvar2);
					currentcode.bus.ExternalDevices[tempvar1].PutMatrix(
						currentcode.matrixmemory[tempvar2]);
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.DATA] = 1;
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				}
				else
				{
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.DATA] =-2;
					currentthread.pstack[currentthread.stackpointer,
						(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
				}

			}
			catch(Exception e)
			{
				currentthread.pstack[currentthread.stackpointer,
					(int)MEMCONSTANTS.DATA] =-1;
				currentthread.pstack[currentthread.stackpointer,
					(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;
			}

			if(tempvar2type == MEMTYPECONSTANTS.TEMPREFERENCE)
				FreeMatrixMemory(tempvar2);
		}
		private void ASM_IsMatrix(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
	
			currentthread.stackpointer++;
			if(tempvar1type == MEMTYPECONSTANTS.REFERENCE 
				|| tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE) 
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =1;
			else
				currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA] =0;    
			
			currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;

			if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)
				FreeMatrixMemory(tempvar1);
		}
		private void ASM_GetRows(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
	
		
			if(tempvar1type != MEMTYPECONSTANTS.REFERENCE 
				&& tempvar1type != MEMTYPECONSTANTS.TEMPREFERENCE)
				throw new Exception("Type mismatch");

			GetMatrix(tempvar1);
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.DATA] =currentcode.matrixmemory[tempvar1].Rows;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;

			if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)
				FreeMatrixMemory(tempvar1);
		}
		private void ASM_GetColumns(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
	
		
			if(tempvar1type != MEMTYPECONSTANTS.REFERENCE 
				&& tempvar1type != MEMTYPECONSTANTS.TEMPREFERENCE)
				throw new Exception("Type mismatch");

			GetMatrix(tempvar1);
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.DATA] =currentcode.matrixmemory[tempvar1].Columns;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.VALUE;

			if(tempvar1type == MEMTYPECONSTANTS.TEMPREFERENCE)
				FreeMatrixMemory(tempvar1);
		}
		private void ASM_GetPrimes(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
	
		
			if(tempvar1type != MEMTYPECONSTANTS.VALUE)
				throw new Exception("Type mismatch");

			if(tempvar1 <= 0)
				throw new Exception("Supplied argument cannot be less than or equal to zero.");
		
			long num =1;
			Matrix tempmat = new Matrix(1,tempvar1);

			for(int lpvar=1;lpvar<=tempvar1;lpvar++)
			{
				bool primeflag = false;
				
				while(!primeflag)
				{
					long k=1;
					for (int lpvar2=2;lpvar2<=num/2;lpvar2++)
					{
						k=num%lpvar2;
						if(k==0)
							break;
					}
					if(k!=0)
					{
						tempmat.m_MatArr[0,lpvar-1] = num;
						primeflag=true;
					}
					num++;
				}
			}
					
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.DATA] =PlaceMatrix(tempmat);;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.TEMPREFERENCE;
		}
		private void ASM_GetPrimesUpto(ref ProgramThread currentthread)
		{
			int tempvar1;
			MEMTYPECONSTANTS tempvar1type;

			tempvar1 = (int)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.DATA];			
			tempvar1type =(MEMTYPECONSTANTS)currentthread.pstack[currentthread.stackpointer,(int)MEMCONSTANTS.TYPE];			
			currentthread.stackpointer--;
	
		
			if(tempvar1type != MEMTYPECONSTANTS.VALUE)
				throw new Exception("Type mismatch");

			if(tempvar1 <= 0)
				throw new Exception("Supplied argument cannot be less than or equal to zero.");
		
			
			ArrayList templist = new ArrayList();

			for(int lpvar=1;lpvar<=tempvar1;lpvar++)
			{
				long k=1;
				for (int lpvar2=2;lpvar2<=lpvar/2;lpvar2++)
				{
					k=lpvar%lpvar2;
					if(k==0)
						break;
				}
				if(k!=0)
					templist.Add(lpvar);
			}
			
			Matrix tempmat = new Matrix(1,templist.Count);
			int lpvar3=0;
			foreach(object tmp in templist)
			{
				tempmat.m_MatArr[0,lpvar3] =long.Parse(tmp.ToString());
				lpvar3++;
			}
			
			currentthread.stackpointer++;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.DATA] =PlaceMatrix(tempmat);;
			currentthread.pstack[currentthread.stackpointer,
				(int)MEMCONSTANTS.TYPE] =(double)MEMTYPECONSTANTS.TEMPREFERENCE;
		}
		/*private void ASM_ParallelStart(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			currentthread.inspointer = (int)currentcode.numconstants[
				currentcode.instructions[currentthread.inspointer]];

			WrappedThreadObject tempthread = new WrappedThreadObject(this);
			ArrayList threads= new ArrayList();
			while(currentcode.instructions[currentthread.inspointer]==(ushort)ASMPROCESSOR_OPERATIONS.CREATETHREAD)
			{
				currentthread.inspointer++;
				tempthread.mthread.inspointer = (int)currentcode.numconstants[
					currentcode.instructions[currentthread.inspointer]];
				currentthread.inspointer++;
				tempthread.mthread.endthread = (int)currentcode.numconstants[
					currentcode.instructions[currentthread.inspointer]];
				currentthread.inspointer++;
				threads.Add(tempthread);
				tempthread.Start();
				tempthread = new WrappedThreadObject(this);
			}
			foreach(WrappedThreadObject temp in threads)
				temp.thread.Join();

			//threads = null;
		}*/
		private void ASM_ParallelStart(ref ProgramThread currentthread)
		{
			currentthread.inspointer++;
			int paralleloffset= currentthread.inspointer;
			currentthread.inspointer += (int)currentcode.numconstants[
				currentcode.instructions[currentthread.inspointer]];

			WrappedThreadObjectRS tempthread = new WrappedThreadObjectRS(this);
			ArrayList threads= new ArrayList();
			
			while(currentcode.instructions[currentthread.inspointer]==(ushort)ASMPROCESSOR_OPERATIONS.CREATETHREAD)
			{
					currentthread.inspointer++;
					tempthread.mthread.inspointer =paralleloffset + (int)currentcode.numconstants[
						currentcode.instructions[currentthread.inspointer]];
					currentthread.inspointer++;
					tempthread.mthread.endthread =paralleloffset + (int)currentcode.numconstants[
						currentcode.instructions[currentthread.inspointer]];
					currentthread.inspointer++;
				

					currentthread.inspointer += (currentcode.instructions[currentthread.inspointer]+1);
					threads.Add(tempthread);
					tempthread.Start();
					tempthread = new WrappedThreadObjectRS(this);
			}
			foreach(WrappedThreadObjectRS temp in threads)
				temp.thread.Join();

			//threads = null;
		}
	}
	

	public class WrappedThreadObjectRS
	{
		
		public ProgramThread mthread;
		ASMProcessorRS threadprocessor;
		public Thread thread;
        
		public WrappedThreadObjectRS(ASMProcessorRS threadprocessor)
		{
			this.mthread = new ProgramThread();
			this.mthread.pstack = new double[RemoteMachine.threadstacksize,2];
			this.mthread.stackpointer = -1;
			this.threadprocessor = threadprocessor;
		}
		public void Start()
		{
			thread = new Thread(new ThreadStart(this.ThreadProc));
			thread.Start();
		}
		public void ThreadProc()
		{
			try
			{
				threadprocessor.Process(ref mthread);
			}
			catch(Exception ee)
			{
				//System.Windows.Forms.MessageBox.Show("Thread - " + mthread.inspointer 
				//	+ " -> " + mthread.endthread + "   :" + ee.Message);
				//Console.WriteLine("Thread - " + mthread.inspointer 
				//	+ " -> " + mthread.endthread + "   :" + ee.Message);
				threadprocessor.raiseerror(ee.Message);
			}
		}
	}
}
