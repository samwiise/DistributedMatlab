using System;
using System.IO;
using System.Collections;
using ASMDM;

namespace DMatlab
{
	public class CodeGenerator
	{
		
		public static void GenerateCode(ArrayList InstructionCode,NumberAndIdentifierTable nids,SyntaxTree OperationTree){
		
			switch(OperationTree.NodeType){
			
				case SyntaxTreeNodeType.BLOCK:
					
					if(OperationTree.NodeValue=="StatementBlock")
					{
						foreach(SyntaxTree stmt in OperationTree.ChildNodes)
							GenerateCode(InstructionCode,nids,stmt);
					}
					else if(OperationTree.NodeValue=="ParallelBlock"){
						if(OperationTree.ChildNodes.Count>0){
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.PARALLELSTART);
							int temp = 	InstructionCode.Add(0);
							ArrayList templist=new ArrayList();
							int temp2,temp3;
							foreach(SyntaxTree stmt in OperationTree.ChildNodes)
							{
								temp2 = InstructionCode.Count-temp;
								GenerateCode(InstructionCode,nids,stmt);
								temp3 = (InstructionCode.Count-1)-temp;
								if(temp3>=temp2)
								{
									ArrayList templist2=new ArrayList();
									ushort maxref=0;
									SearchForMemoryAccess(templist2,stmt,nids,ref maxref);
									templist.Add((ushort)ASMPROCESSOR_OPERATIONS.CREATETHREAD);
									templist.Add(nids.GetLocation(temp2));
									templist.Add(nids.GetLocation(temp3));
									templist.Add(templist2.Count+1);
									templist.Add(maxref);
									AppendList(templist,templist2);
								}
							}
							InstructionCode[temp] = nids.GetLocation(InstructionCode.Count-temp);
							AppendList(InstructionCode,templist);
							templist = null;
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.PARALLELEND);
						}
					}else
						throw new Exception("Invalid Operation");
					
					break;

				case SyntaxTreeNodeType.FUNCTION:
					switch(OperationTree.NodeValue)
					{

						case "DefineMatrix":
							ProcessDefineMatrix(InstructionCode,nids,OperationTree);
							break;
						case "Transpose":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Transpose takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.TRANSPOSE);
							break;

						case "Adjoint":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Adjoint takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ADJOINT);
							break;

						case "NewMatrix":
							if(OperationTree.ChildNodes.Count !=2)throw new Exception("NewMatrix takes two arguments.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.NEWMATRIX);
							break;

						case "NewIdentityMatrix":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("NewIdentityMatrix takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.NEWIDENTITYMATRIX);
							break;

						case "Sin":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Sin takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.SIN);
							break;
						case "Cos":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Cos takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.COS);
							break;
						case "Tan":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Tan takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.TAN);
							break;
						case "ArcTan":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("ArcTan takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ARCTAN);
							break;
						case "ArcSin":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("ArcSin takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ARCSIN);
							break;
						case "ArcCos":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("ArcCos takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ARCCOS);
							break;

						case "GetPrimeNumbers":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("GetPrimeNumbers takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETPRIMES);
							break;
						case "GetPrimeNumbersUpto":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("GetPrimeNumbersUnder takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETPRIMESUPTO);
							break;
						case "Sqrt":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("Sqrt takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.SQRT);
							break;
						case "IsMatrix":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("IsMatrix takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISMATRIX);
							break;
						case "GetRows":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("GetRows takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETROWS);
							break;
						case "GetColumns":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("GetColumns takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETCOLUMNS);
							break;
						case "InputNumber":
							for(int a = OperationTree.ChildNodes.Count-1;a>=0;a--)
							{
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[a]);
							}
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.INPUTNUMBER);
							InstructionCode.Add((ushort)OperationTree.ChildNodes.Count);
							break;

						case "WaitForDevice":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("WaitForDevice takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.WAITFORDEVICE);
							break;
						case "GetMatrixFromDevice":
							if(OperationTree.ChildNodes.Count !=1)throw new Exception("GetMatrixFromDevice takes one argument.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMATRIXFROMDEVICE);
							break;
						case "SetMatrixToDevice":
							if(OperationTree.ChildNodes.Count !=2)throw new Exception("SetMatrixToDevice takes two arguments.");
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.SETMATRIXTODEVICE);
							break;

						default:
							throw new Exception("Invalid Function");
					}
					break;

				case SyntaxTreeNodeType.STRUCTURE:
					switch(OperationTree.NodeValue)
					{
						
						case "if":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMPCMP);
							int temp = 	InstructionCode.Add(0);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							if(OperationTree.ChildNodes.Count==3)
							{
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMP);
								InstructionCode[temp] = nids.GetLocation(InstructionCode.Count-temp);
								temp = InstructionCode.Add(0);
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[2]);
								InstructionCode[temp] = nids.GetLocation((InstructionCode.Count-temp)-1);
							}else
								InstructionCode[temp] = nids.GetLocation((InstructionCode.Count-temp)-1);

							break;
						
						case "while":
							temp = InstructionCode.Count;
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMPCMP);
							int temp2 =	InstructionCode.Add(0);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMP);
							InstructionCode.Add(nids.GetLocation(-((InstructionCode.Count - temp)+1)));
							InstructionCode[temp2] = nids.GetLocation((InstructionCode.Count-temp2)-1);
							break;
						
						case "do":
							temp = InstructionCode.Count;
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMPCMP2);
							InstructionCode.Add(nids.GetLocation(-((InstructionCode.Count - temp)+1)));
							break;
						case "for":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							temp = InstructionCode.Count;
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMPCMP);
							temp2 =	InstructionCode.Add(0);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[3]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[2]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.JMP);
							InstructionCode.Add(nids.GetLocation(-((InstructionCode.Count - temp)+1)));
							InstructionCode[temp2] = nids.GetLocation((InstructionCode.Count-temp2)-1);
							break;

						default:
							throw new Exception("Invalid Structure");
					}
					break;

				case SyntaxTreeNodeType.IDENTIFIER:
					if(OperationTree.ChildNodes.Count==2)
					{
						GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
						GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
						InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMEMORY);
						InstructionCode.Add(nids.GetLocation(OperationTree.NodeValue));
						InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMATRIXELEMENT);
					}
					else
					{
						InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMEMORY);
						InstructionCode.Add(nids.GetLocation(OperationTree.NodeValue));
					}
					
					
					break;
				case SyntaxTreeNodeType.PROCEDURE:
					switch(OperationTree.NodeValue)
					{

						case "Print":
							for(int a = OperationTree.ChildNodes.Count-1;a>=0;a--){
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[a]);
							}
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.PRINT);
							InstructionCode.Add((ushort)OperationTree.ChildNodes.Count);
							break;
					}
					break;

				case SyntaxTreeNodeType.NUMBER:
					InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETCONSTANT);
					InstructionCode.Add(nids.GetLocation(double.Parse(OperationTree.NodeValue)));
					break;
				case SyntaxTreeNodeType.LITERAL:
					InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.PUSHSTACK);
					InstructionCode.Add(nids.GetLiteralLocation(OperationTree.NodeValue));
					InstructionCode.Add((ushort)MEMTYPECONSTANTS.LITERAL);
					break;

				case SyntaxTreeNodeType.OPERATOR:
					switch(OperationTree.NodeValue){
						
                        case "+":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ADD);
							break;
						case "*":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.MULTIPLY);
                        	break;
						case "/":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.DIVIDE);
							break;
						case "^":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.EXPONENT);
							break;
						case "%":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.MOD);
							break;
						case "-":
							if(OperationTree.ChildNodes.Count==2)
							{
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.SUBTRACT);
							}
							else if(OperationTree.ChildNodes.Count==1){
								GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.NEGATIVE);
							}
							break;
						case "--":
						case "++":
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.PUSHSTACK);
							InstructionCode.Add(1);
							InstructionCode.Add((ushort)MEMTYPECONSTANTS.VALUE);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							if(OperationTree.NodeValue == "++")
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ADD);
							else
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.SUBTRACT);
							SyntaxTree LeftOperand = (SyntaxTree)OperationTree.ChildNodes[0];
							if(LeftOperand.ChildNodes.Count==2)
							{
								GenerateCode(InstructionCode,nids,(SyntaxTree)LeftOperand.ChildNodes[1]);
								GenerateCode(InstructionCode,nids,(SyntaxTree)LeftOperand.ChildNodes[0]);
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMEMORY);
								InstructionCode.Add(nids.GetLocation(LeftOperand.NodeValue));
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STOREMATRIX);
							}
							else
							{
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STORE);
								InstructionCode.Add(nids.GetLocation(LeftOperand.NodeValue));
							}
							break;
												
						case "=":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							LeftOperand = (SyntaxTree)OperationTree.ChildNodes[0];
							if(LeftOperand.ChildNodes.Count==2)
							{
								GenerateCode(InstructionCode,nids,(SyntaxTree)LeftOperand.ChildNodes[1]);
								GenerateCode(InstructionCode,nids,(SyntaxTree)LeftOperand.ChildNodes[0]);
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.GETMEMORY);
								InstructionCode.Add(nids.GetLocation(LeftOperand.NodeValue));
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STOREMATRIX);
							}
							else{
								InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STORE);
								InstructionCode.Add(nids.GetLocation(LeftOperand.NodeValue));
							}
						
							break;
						case "==":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISEQUAL);
							break;
						case "!=":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISNOTEQUAL);
							break;
						case ">":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISGREATERTHAN);
							break;
						case "<":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISLESSTHAN);
							break;
						case ">=":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISGREATERTHANEQUAL);
							break;
						case "<=":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ISLESSTHANEQUAL);
							break;
						case "&&":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STOREEXTRA);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.LOGICALAND);
							break;
						case "||":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[1]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STOREEXTRA);
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.LOGICALOR);
							break;
						case "!":
							GenerateCode(InstructionCode,nids,(SyntaxTree)OperationTree.ChildNodes[0]);
							InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.LOGICALNOT);
							break;

					}
					break;
			
			}
		}
		public static void SearchForMemoryAccess(ArrayList addresses,SyntaxTree OperationTree,NumberAndIdentifierTable nids,ref ushort maxref){
			
			switch(OperationTree.NodeType){
				
				case SyntaxTreeNodeType.IDENTIFIER:
					ushort tmp =  nids.GetLocation(OperationTree.NodeValue);
					if(addresses.Contains(tmp)==false)
					{
						addresses.Add(tmp);
						if(tmp>maxref)maxref=tmp;
					}
					break;
			}
			foreach(SyntaxTree stmt in OperationTree.ChildNodes)
				SearchForMemoryAccess(addresses,stmt,nids,ref maxref);
		}
		private static void AppendList(ArrayList destination,ArrayList source){
			
			//foreach(ushort tmp in source)
			//	destination.Add(source);
			destination.AddRange(source);
		}
		private static void ProcessDefineMatrix(ArrayList InstructionCode,NumberAndIdentifierTable nids,SyntaxTree OperationTree)
		{
			
			int rws = OperationTree.ChildNodes.Count;
			int cols;

			SyntaxTree temp = (SyntaxTree)OperationTree.ChildNodes[0];
			temp = (SyntaxTree)temp.ChildNodes[0];

			if(temp.NodeType!=SyntaxTreeNodeType.NUMBER)
			{
				double st,ed,inc;
				SyntaxTree newtemp = (SyntaxTree)temp.ChildNodes[0];
				st = double.Parse(newtemp.NodeValue);
				
				newtemp = (SyntaxTree)temp.ChildNodes[1];
				inc = double.Parse(newtemp.NodeValue);

				newtemp = (SyntaxTree)temp.ChildNodes[2];
				ed = double.Parse(newtemp.NodeValue);

				cols = (int)((ed-st)/inc)+1;
			}
			else
			{
				temp =(SyntaxTree)OperationTree.ChildNodes[0];
				cols = temp.ChildNodes.Count;
			}
			InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.STARTMATRIX);
			InstructionCode.Add((ushort)rws);
			InstructionCode.Add(nids.GetLocation(cols));
			
			
			foreach(SyntaxTree tmp in OperationTree.ChildNodes)
			{
				cols=1;
				foreach(SyntaxTree tmp2 in tmp.ChildNodes)
				{
					if(tmp2.NodeType == SyntaxTreeNodeType.NUMBER)
					{
						if (cols==1)InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ROWDEFINEDBYVALUES);
						InstructionCode.Add(nids.GetLocation(double.Parse(tmp2.NodeValue)));
						cols++;

					}
					else
					{
						double st,inc;
						
						SyntaxTree newtemp = (SyntaxTree)tmp2.ChildNodes[0];
						st = double.Parse(newtemp.NodeValue);
				
						newtemp = (SyntaxTree)tmp2.ChildNodes[1];
						inc = double.Parse(newtemp.NodeValue);

						
						InstructionCode.Add((ushort)ASMPROCESSOR_OPERATIONS.ROWDEFINEDBYRANGE);
						InstructionCode.Add(nids.GetLocation(st));
						InstructionCode.Add(nids.GetLocation(inc));

					}
				}
			}
		}

		public static void SaveFile(ArrayList InstructionCode,NumberAndIdentifierTable nids,string filename)
		{
			
			FileStream fs = new FileStream(filename,FileMode.Create,FileAccess.Write);
			BinaryWriter wr = new BinaryWriter(fs);
			
			ushort filecode = 1508;
			wr.Write(filecode);
			wr.Write(nids.identifiers.Count);

			double[] numcs = nids.GetNumbers();
			string[] literals = nids.GetLiterals();
			
			int nmlen = Buffer.ByteLength(numcs);

			byte[] buffer = new byte[nmlen];
			Buffer.BlockCopy(numcs,0,buffer,0,nmlen);
			wr.Write(nmlen); 
			wr.Write(buffer); 

			string literal = String.Join("\x00",literals);
			wr.Write(System.Text.ASCIIEncoding.ASCII.GetByteCount(literal));
			wr.Write(System.Text.ASCIIEncoding.ASCII.GetBytes(literal));
			
			wr.Write(InstructionCode.Count*2);
				
			for(int a=0;a<InstructionCode.Count;a++)
				wr.Write(ushort.Parse(InstructionCode[a].ToString()));

			wr.Close();
			fs.Close();
		}
		
	}
}
