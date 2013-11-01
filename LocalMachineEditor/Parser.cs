using System;

namespace DMatlab
{
	/// <summary>
	/// Summary description for Parser.
	/// </summary>
	public enum TokenType{
		IDENTIFIER,OPERATOR,NUMBER,FUNCTION,LPARAN,RPARAN,LSQUARE,RSQUARE,COLON,KEYWORD,PROCEDURE,
		COMMA,ERROR,END,LCURBRACKET,RCURBRACKET,SEMICOLON,DIRECTIVE,REL_OPERATOR,LOGICAL_OPERATOR,
		LITERAL,COMMENTS,INCREMENTDECREMENT
	}
	public struct Token{
		public TokenType TType;
		public string TokenValue;
		public long startpointer;
		public long endpointer;

		public Token(TokenType ttype,string tvalue){
			TType = ttype;
			TokenValue = tvalue;
			startpointer=0;
			endpointer=0;
		}
	}

	public enum States{
		S_START,S_DONE,S_IDENTIFIER,S_NUMBER,S_COMMENT,S_DIRECTIVE,S_INCREMENT,S_DECREMENT,
		S_COMMENTBLOCK,S_COMMENTLINE,S_COMMENTBLOCKEND,S_ISEQUAL,S_LESSTHAN,S_GREATORTHAN,S_NOTEQUAL,
		S_AND,S_OR,S_LITERAL
	}

	public class Parser
	{
		private static char[] buffer;
		private static long pointer;
		public  static System.Collections.ArrayList Tokens = new System.Collections.ArrayList();
		
		private static string ReserveWords =",Sin,Cos,Tan,ArcSin,ArcCos,ArcTan,DefineMatrix,NewMatrix,NewIdentityMatrix,Transpose,Adjoint,InputNumber,GetMatrixFromDevice,SetMatrixToDevice,WaitForDevice,IsMatrix,GetRows,GetColumns,GetPrimeNumbers,GetPrimeNumbersUpto,Sqrt,";
		private static string Procedures = ",Print,";
		private static string KeyWords = ",if,else,sequential,parallel,while,for,do,";

		
		public static  void ClearTokens(){
			Tokens.Clear();
			//PolishStack.Clear();
		}
		private static Token GetToken(){
			
			States curstate;
			char curchar;
            Token curtoken = new Token();
			bool savechar;
			bool dotflag = false;
		
			curtoken.TokenValue = "";
			curstate = States.S_START;

			while(curstate != States.S_DONE){
				curchar = GetNextCharacter();
				savechar = true;

				switch(curstate){
					
					case States.S_START:
						if(IsWhiteSpace(curchar))
						{
							savechar = false;
						}
						else if (IsDigit(curchar))
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_NUMBER;
						}
						else if (curchar=='.')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_NUMBER;
							dotflag = true;
						}
						else if (curchar=='#')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_DIRECTIVE;
						}
						else if (curchar=='='){
							curtoken.startpointer = pointer-1;
							curstate = States.S_ISEQUAL;
						}
						else if (curchar=='>')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_GREATORTHAN;
						}
						else if (curchar=='<')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_LESSTHAN;
						}
						else if (curchar=='!')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_NOTEQUAL;
						}
						else if (curchar=='&')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_AND;
						}
						else if (curchar=='|')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_OR;
						}
						else if (curchar=='/')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_COMMENT;
						}
						else if (curchar=='+')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_INCREMENT;
						}
						else if (curchar=='-')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_DECREMENT;
						}
						else if (IsLetter(curchar))
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_IDENTIFIER;
						}	
						else if (curchar=='"')
						{
							curtoken.startpointer = pointer-1;
							curstate = States.S_LITERAL;
							savechar = false;
						}
						else
						{
							curtoken.startpointer = pointer-1;
							curtoken.endpointer = pointer;
							curstate=States.S_DONE;
							switch(curchar)
							{
								
								//case '+':
								//case '-':
								case '*':
								case '^':
								case '%':
									curtoken.TType = TokenType.OPERATOR;
									break;
								case ':':
									curtoken.TType = TokenType.COLON;
									break;
								case ';':
									curtoken.TType = TokenType.SEMICOLON;
									break;
								case '(':
									curtoken.TType = TokenType.LPARAN;
									break;
								case ')':
									curtoken.TType = TokenType.RPARAN;
									break;
								case '[':
									curtoken.TType = TokenType.LSQUARE;
									break;
								case ']':
									curtoken.TType = TokenType.RSQUARE;
									break;
								case '{':
									curtoken.TType = TokenType.LCURBRACKET;
									break;
								case '}':
									curtoken.TType = TokenType.RCURBRACKET;
									break;
								case ',':
									curtoken.TType = TokenType.COMMA;
									break;
								case (char)0:
									curtoken.TType = TokenType.END;
									break;
								default:
									curtoken.TType = TokenType.ERROR;
									break;
							}
						}
						break;
					
					case States.S_LITERAL:
						if(curchar=='"')
						{
							savechar = false;
							curtoken.endpointer = pointer;
							curtoken.TType = TokenType.LITERAL;
							curstate = States.S_DONE;
						}
						else if(curchar==0){
							UnGetNextCharacter();
							savechar = false;
							curtoken.endpointer = pointer;
							curtoken.TType = TokenType.ERROR;
							curstate = States.S_DONE;
						}
						break;

					case States.S_AND:
						if(curchar=='&')
							curtoken.TType = TokenType.LOGICAL_OPERATOR;
						else
						{
							UnGetNextCharacter();
							savechar = false;
							curtoken.TType = TokenType.ERROR;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;
					
					case States.S_OR:
						if(curchar=='|')
							curtoken.TType = TokenType.LOGICAL_OPERATOR;
						else
						{
							UnGetNextCharacter();
							savechar = false;
							curtoken.TType = TokenType.ERROR;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;

					case States.S_ISEQUAL:
						if(curchar=='=')
							curtoken.TType = TokenType.REL_OPERATOR;
						else
						{
							UnGetNextCharacter();
							savechar = false;
							curtoken.TType = TokenType.OPERATOR;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;
					
					case States.S_NOTEQUAL:
						if(curchar=='=')
							curtoken.TType = TokenType.REL_OPERATOR;
						else
						{
							UnGetNextCharacter();
							savechar = false;
							curtoken.TType = TokenType.LOGICAL_OPERATOR;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;
					case States.S_LESSTHAN:
						curtoken.TType = TokenType.REL_OPERATOR;
						if(curchar!='='){
							UnGetNextCharacter();
							savechar = false;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;
					case States.S_GREATORTHAN:
						curtoken.TType = TokenType.REL_OPERATOR;
						if(curchar!='=')
						{
							UnGetNextCharacter();
							savechar = false;
						}
						curtoken.endpointer = pointer;
						curstate = States.S_DONE;
						break;


					case States.S_INCREMENT:
						if(curchar=='+')
							curtoken.TType = TokenType.INCREMENTDECREMENT;
						else
						{
							savechar=false;
							UnGetNextCharacter();
							curtoken.TType =  TokenType.OPERATOR;
						}
						curstate = States.S_DONE;
						curtoken.endpointer = pointer;
						break;

					case States.S_DECREMENT:
						if(curchar=='-')
							curtoken.TType = TokenType.INCREMENTDECREMENT;
						else
						{
							savechar=false;
							UnGetNextCharacter();
							curtoken.TType =  TokenType.OPERATOR;
						}
						curstate = States.S_DONE;
						curtoken.endpointer = pointer;
						break;

					case States.S_COMMENT:
						savechar = false;
						if(curchar=='/')
							curstate = States.S_COMMENTLINE; 
						else if(curchar=='*')
							curstate = States.S_COMMENTBLOCK;
						else{
							UnGetNextCharacter();
							curstate = States.S_DONE;
							curtoken.TType =  TokenType.OPERATOR;
							curtoken.endpointer = pointer;
						}
						break;

					case States.S_COMMENTLINE:
						savechar=false;
						if(curchar==13 || curchar==0 || curchar==10){
							curtoken.TokenValue = "Commented Line";
							curtoken.TType = TokenType.COMMENTS;
							curstate = States.S_DONE;
							curtoken.endpointer = pointer;
						}
						break;
					
					case States.S_COMMENTBLOCK:
						savechar=false;
						if(curchar=='*')
							curstate = States.S_COMMENTBLOCKEND;
						else if(curchar==0){
							curstate = States.S_DONE;
							curtoken.TokenValue = "Commented Block";
							curtoken.TType = TokenType.COMMENTS;
							curtoken.endpointer = pointer;
						}
						break;
					
					case States.S_COMMENTBLOCKEND:
						savechar=false;
						if(curchar=='/')
						{
							curtoken.TokenValue = "Commented Block";
							curstate = States.S_DONE;
							curtoken.TType = TokenType.COMMENTS;
							curtoken.endpointer = pointer;
						}else
							curstate = States.S_COMMENTBLOCK;
						break;

					case States.S_IDENTIFIER:
						if(!IsDigit(curchar) && !IsLetter(curchar)){
							UnGetNextCharacter();
							savechar =false;
							curstate = States.S_DONE;
							curtoken.endpointer = pointer;
							curtoken.TType = TokenType.IDENTIFIER;
						}
						break;
					case States.S_DIRECTIVE:
						if(!IsLetter(curchar)){
							if(IsWhiteSpace(curchar))
							{
								UnGetNextCharacter();
								savechar =false;
								curtoken.TType = TokenType.DIRECTIVE;
							}
							else
								curtoken.TType = TokenType.ERROR;
							
							curstate = States.S_DONE;
							curtoken.endpointer = pointer;
						}
						break;

					case States.S_NUMBER:
						if(!IsDigit(curchar))
						{
				//			if(IsWhiteSpace(curchar) || curchar == ':' || curchar==')'  || curchar==']' || curchar==0 || curchar==',' || curchar=='}')
				//			{
							if(curchar=='.')
							{
								if(dotflag==false)
									dotflag=true;
								else
								{
									curstate = States.S_DONE;
									curtoken.TType = TokenType.ERROR;
									curtoken.endpointer = pointer;
								}
							}
							else
							{
								UnGetNextCharacter();
								savechar =false;
								curstate = States.S_DONE;
								curtoken.TType = TokenType.NUMBER;
								curtoken.endpointer = pointer;
							}
				//			}
				//			else{
				//				UnGetNextCharacter();
				//				savechar =false;
				//				curstate = States.S_DONE;
				//				curtoken.TType = TokenType.ERROR;
				//			}

						}
						break;
	
				}
				if(savechar)curtoken.TokenValue = curtoken.TokenValue+curchar;
			}
			
			if(curtoken.TType == TokenType.IDENTIFIER)
			{
				if(ReserveWords.IndexOf(","+curtoken.TokenValue+",")!=-1)
					curtoken.TType  = TokenType.FUNCTION;
				else if(KeyWords.IndexOf(","+curtoken.TokenValue+",")!=-1)
					curtoken.TType  = TokenType.KEYWORD;
				else if(Procedures.IndexOf(","+curtoken.TokenValue+",")!=-1)
					curtoken.TType  = TokenType.PROCEDURE;

			}
			
			return curtoken;
		}
		public static bool Parse(char[] data){

			bool rtval = true;

			ClearTokens();
			buffer = data;
			pointer= 0;
			while(pointer<buffer.Length){
				Token temptoken =  GetToken();
				if(temptoken.TType == TokenType.ERROR)rtval=false;
				if (temptoken.TType != TokenType.COMMENTS) Tokens.Add(temptoken);
			}
			return rtval;
		}
		
		private static char GetNextCharacter(){
			if(pointer < buffer.Length){
				return buffer[pointer++];
			}
			pointer++;
			return (char)0;
		}
		private static void UnGetNextCharacter()
		{
			if(pointer>0)pointer--;
		}
		private static bool IsWhiteSpace(char c){
			if(c==32 || c==13 || c==9 || c==10)
				return true;
			return false;
		}
		private static bool IsDigit(char c)
		{
			if(c>=48 && c<=57)
				return true;
			return false;
		}
		private static bool IsLetter(char c)
		{
			if((c>=65 && c<=90)||(c>=97 && c<=122))
				return true;
			return false;
		}
		

	}
}
