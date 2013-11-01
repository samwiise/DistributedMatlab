using System;

namespace DMatlab
{
	public class SyntaxAnalyzer
	{
	
		public static Token CurrentToken;
		private static System.Collections.IEnumerator TokenEnm;


		private static void GetToken(){
			if (TokenEnm.MoveNext())
				CurrentToken = (Token)TokenEnm.Current;
			else
				CurrentToken = new Token(TokenType.END,"End");
		}
		private static void match(TokenType ExpectedTokenType,string ExpectedTokenValue){
			if(CurrentToken.TType == ExpectedTokenType && CurrentToken.TokenValue == ExpectedTokenValue)
				GetToken();
			else
				Error();
		}

		private static void Error(){
			//System.Windows.Forms.MessageBox.Show("Error -- CurrentToken : " + CurrentToken.TType.ToString() + " : " + CurrentToken.TokenValue);
			throw (new Exception(CurrentToken.TokenValue));
		}

		public static SyntaxTree GetTree(System.Collections.ArrayList Tokens){
			
			TokenEnm = Tokens.GetEnumerator();
			
			GetToken();

			return StatementBlock();
		}
		
		private static SyntaxTree StatementBlock(){
		
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.BLOCK,"StatementBlock");

			while(CurrentToken.TType != TokenType.END)
				if(CurrentToken.TType == TokenType.DIRECTIVE){
					temp.AddChild(Directive());
				}else
					temp.AddChild(Statement());

			return temp;
		
		}
		private static SyntaxTree StatementBlock2(){
			
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.BLOCK,"StatementBlock");
			
			match(TokenType.LCURBRACKET,"{");			
						
			while(CurrentToken.TType != TokenType.RCURBRACKET)
				if(CurrentToken.TType == TokenType.DIRECTIVE)
				{
					temp.AddChild(Directive());
				}
				else
					temp.AddChild(Statement());

			match(TokenType.RCURBRACKET,"}");			
			return temp;
		
		}
		private static SyntaxTree Directive(){
			
			match(TokenType.DIRECTIVE,"#PARALLEL");
			
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.BLOCK,"ParallelBlock");
			temp.AddChild(Statement());
			while(CurrentToken.TType != TokenType.DIRECTIVE)
				temp.AddChild(Statement());
			
			match(TokenType.DIRECTIVE,"#ENDPARALLEL");

			return temp;
		}
		private static SyntaxTree ParallelBlock()
		{
			
			match(TokenType.KEYWORD ,"parallel");
			
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.BLOCK,"ParallelBlock");
			
			match(TokenType.LCURBRACKET,"{");			
						
			while(CurrentToken.TType != TokenType.RCURBRACKET)
				if(CurrentToken.TType == TokenType.DIRECTIVE)
				{
					temp.AddChild(Directive());
				}
				else
					temp.AddChild(Statement());
			match(TokenType.RCURBRACKET,"}");			
			return temp;
		}

		private static SyntaxTree Statement()
		{
		
			SyntaxTree temp;
			switch(CurrentToken.TType)
			{
			
				case TokenType.IDENTIFIER:
					temp= AssignmentExpression();
					match(TokenType.SEMICOLON,";");
					break;
				case TokenType.FUNCTION:
					temp= FunctionCall();
					match(TokenType.SEMICOLON,";");
					break;
				case TokenType.PROCEDURE:
					temp= Procedure();
					match(TokenType.SEMICOLON,";");
					break;
				case TokenType.KEYWORD:
					switch(CurrentToken.TokenValue){
					
						case "if":
							temp = IfKeyWord();				
							break;
						case "for":
							temp = ForKeyWord();
							break;
						case "while":
							temp = WhileKeyWord();
                            break;
						case "do":
							temp = DoKeyWord();
							break;
						case "parallel":
							temp = ParallelBlock();
							break;
						case "sequential":
							match(TokenType.KEYWORD ,"sequential");
							temp = StatementBlock2();
							break;
						default:
							Error();
							temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OTHER,"ERROR");
							break;
					}
					break;
				default:
					Error();
					temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OTHER,"ERROR");
					break;
			}
			
			
			return temp;
		}
		private static SyntaxTree ForKeyWord()
		{
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.STRUCTURE,"for");	
			match(TokenType.KEYWORD,"for"); 
			match(TokenType.LPARAN,"(");

					
			temp.AddChild(AssignmentExpression());
			match(TokenType.SEMICOLON,";");
	
			temp.AddChild(Conditions());
			match(TokenType.SEMICOLON,";");
			
			temp.AddChild(AssignmentExpression());

			
			match(TokenType.RPARAN,")");

			if(CurrentToken.TType == TokenType.LCURBRACKET)
				temp.AddChild(StatementBlock2());
			else 
				temp.AddChild(Statement());
			
			return temp;
		}
		private static SyntaxTree WhileKeyWord()
		{
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.STRUCTURE,"while");	
			match(TokenType.KEYWORD,"while"); 
			match(TokenType.LPARAN,"(");
			temp.AddChild(Conditions());
			match(TokenType.RPARAN,")");

			if(CurrentToken.TType == TokenType.LCURBRACKET)
				temp.AddChild(StatementBlock2());
			else 
				temp.AddChild(Statement());
			
			return temp;
		}
		private static SyntaxTree DoKeyWord()
		{
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.STRUCTURE,"do");	
			match(TokenType.KEYWORD,"do"); 
			

			if(CurrentToken.TType == TokenType.LCURBRACKET)
				temp.AddChild(StatementBlock2());
			else 
				temp.AddChild(Statement());
			
			match(TokenType.KEYWORD,"while"); 
			match(TokenType.LPARAN,"(");
			temp.AddChild(Conditions());
			match(TokenType.RPARAN,")");
			match(TokenType.SEMICOLON,";");

			return temp;
		}
		private static SyntaxTree IfKeyWord(){
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.STRUCTURE,"if");	
			match(TokenType.KEYWORD,"if"); 
			match(TokenType.LPARAN,"(");
            temp.AddChild(Conditions());
			match(TokenType.RPARAN,")");

			if(CurrentToken.TType == TokenType.LCURBRACKET)
				temp.AddChild(StatementBlock2());
			else 
				temp.AddChild(Statement());
			
			if(CurrentToken.TType == TokenType.KEYWORD && CurrentToken.TokenValue == "else")
			{
				match(TokenType.KEYWORD,"else"); 
				if(CurrentToken.TType == TokenType.LCURBRACKET)
					temp.AddChild(StatementBlock2());
				else 
					temp.AddChild(Statement());
			}
			return temp;
		}
		private static SyntaxTree Conditions(){
			
			SyntaxTree temp;

			if(CurrentToken.TType == TokenType.LPARAN)
			{
				match(TokenType.LPARAN,"(");
				temp = Conditions();
				match(TokenType.RPARAN,")");
			}else
				temp = TestExpression();
			
			while(CurrentToken.TType ==TokenType.LOGICAL_OPERATOR && CurrentToken.TokenValue !="!")
			{
				SyntaxTree newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);	
				match(TokenType.LOGICAL_OPERATOR,CurrentToken.TokenValue);
			
				newtemp.AddChild(temp);
				if(CurrentToken.TType == TokenType.LPARAN)
				{
					temp = Conditions();
				}
				else
					temp = TestExpression();
				newtemp.AddChild(temp);
				temp = newtemp;
			}
			
			return temp;
		}
		private static SyntaxTree TestExpression(){
			
			SyntaxTree temp;
			if(CurrentToken.TType == TokenType.LPARAN)
			{
				match(TokenType.LPARAN,"(");
				temp = TestExpression();
				match(TokenType.RPARAN,")");
			}
			else if(CurrentToken.TType == TokenType.LOGICAL_OPERATOR && CurrentToken.TokenValue == "!"){
				temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR ,"!");
				match(TokenType.LOGICAL_OPERATOR,"!");
				match(TokenType.LPARAN,"(");
				temp.AddChild(Conditions());
				match(TokenType.RPARAN,")");
			}
			else
			{
				SyntaxTree newtemp = Expression();
				temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(TokenType.REL_OPERATOR,CurrentToken.TokenValue);
				temp.AddChild(newtemp);
				newtemp = Expression();
				temp.AddChild(newtemp);
			}
			return temp;
		}
		private static SyntaxTree AssignmentExpression()
		{
			
			SyntaxTree temp,newtemp;
			temp = Identifier(); // SyntaxTree.MakeNode(SyntaxTreeNodeType.IDENTIFIER,CurrentToken.TokenValue);
			//match(TokenType.IDENTIFIER,CurrentToken.TokenValue);
			if(CurrentToken.TType == TokenType.INCREMENTDECREMENT)
			{
				newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(TokenType.INCREMENTDECREMENT,CurrentToken.TokenValue);
				newtemp.AddChild(temp);
			}
			else
			{
				match(TokenType.OPERATOR ,"=");

				newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,"=");
				newtemp.AddChild(temp);
				newtemp.AddChild(Expression());
			}

			return newtemp;
		}
		
		private static SyntaxTree Expression(){
			
			SyntaxTree temp;
			if(CurrentToken.TType == TokenType.OPERATOR &&  CurrentToken.TokenValue=="-")
			{
				temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(CurrentToken.TType,CurrentToken.TokenValue );
				temp.AddChild(Term());
			}
			else{
				temp= Term();			
			}
 			
			while(CurrentToken.TType == TokenType.OPERATOR && (CurrentToken.TokenValue=="+" || CurrentToken.TokenValue=="-"))
			{
				SyntaxTree newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(CurrentToken.TType,CurrentToken.TokenValue );
				newtemp.AddChild(temp);
				newtemp.AddChild(Term());
				temp = newtemp;
			}

			return temp;
		
		}
		private static SyntaxTree Term(){
			
			SyntaxTree temp = Factor();
			
			while(CurrentToken.TType == TokenType.OPERATOR && (CurrentToken.TokenValue=="*" || CurrentToken.TokenValue=="/")){
				SyntaxTree newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(CurrentToken.TType,CurrentToken.TokenValue );
				newtemp.AddChild(temp);
				newtemp.AddChild(Factor());
				temp = newtemp;
			}

			return temp;
		}
		private static SyntaxTree Factor(){
			
			SyntaxTree temp;
			switch (CurrentToken.TType){
			
				case TokenType.LPARAN:
					match(TokenType.LPARAN,"(");
					temp = Expression();
					match(TokenType.RPARAN,")");
					break;
				case TokenType.NUMBER:
					temp = SyntaxTree.MakeNode (SyntaxTreeNodeType.NUMBER,CurrentToken.TokenValue);
					match(CurrentToken.TType,CurrentToken.TokenValue );
					break;
				case TokenType.IDENTIFIER:
					temp = Identifier(); //SyntaxTree.MakeNode (SyntaxTreeNodeType.IDENTIFIER,CurrentToken.TokenValue);
					//match(CurrentToken.TType,CurrentToken.TokenValue );
					break;
				case TokenType.FUNCTION:
					temp = FunctionCall();
					break;
				default:
					Error();
					temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OTHER,"ERROR");
					break;
			}
			while ((CurrentToken.TokenValue == "^" || CurrentToken.TokenValue == "%") && CurrentToken.TType == TokenType.OPERATOR){
				SyntaxTree newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OPERATOR,CurrentToken.TokenValue);
				match(CurrentToken.TType,CurrentToken.TokenValue );
				newtemp.AddChild(temp);
				if(CurrentToken.TType == TokenType.LPARAN)
				{
					match(TokenType.LPARAN,"(");
					newtemp.AddChild(Expression());
					match(TokenType.RPARAN,")");
					
				}
				else{
					newtemp.AddChild(Factor());
				}
				temp = newtemp;
			}

			return temp;
		}

		private static SyntaxTree FunctionCall(){
		
			if(CurrentToken.TokenValue == "DefineMatrix"){
				return DefineMatrix();
			}
			
			SyntaxTree temp;
            temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.FUNCTION,CurrentToken.TokenValue);
			match(CurrentToken.TType,CurrentToken.TokenValue);

			match(TokenType.LPARAN,"(");

			if(CurrentToken.TType == TokenType.RPARAN){
				match(CurrentToken.TType,")");
				return temp;
			}

			if(CurrentToken.TType == TokenType.LITERAL)
			{
				temp.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.LITERAL,CurrentToken.TokenValue));
				match(CurrentToken.TType,CurrentToken.TokenValue);
			}
			else
				temp.AddChild( Expression());
			

			while(CurrentToken.TType == TokenType.COMMA){
				match(CurrentToken.TType,CurrentToken.TokenValue);
				if(CurrentToken.TType == TokenType.LITERAL)
				{
					temp.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.LITERAL,CurrentToken.TokenValue));
					match(CurrentToken.TType,CurrentToken.TokenValue);
				}
				else
					temp.AddChild( Expression());
			}

			match(TokenType.RPARAN,")");

			return temp;
		}
		private static SyntaxTree Procedure()
		{
					
			SyntaxTree temp;
			temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.PROCEDURE,CurrentToken.TokenValue);
			match(CurrentToken.TType,CurrentToken.TokenValue);

			if(CurrentToken.TType == TokenType.LITERAL){
				temp.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.LITERAL,CurrentToken.TokenValue));
				match(CurrentToken.TType,CurrentToken.TokenValue);
			}
			else
				temp.AddChild( Expression());

			while(CurrentToken.TType == TokenType.COMMA)
			{
				match(CurrentToken.TType,CurrentToken.TokenValue);
				if(CurrentToken.TType == TokenType.LITERAL){
					temp.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.LITERAL,CurrentToken.TokenValue));
					match(CurrentToken.TType,CurrentToken.TokenValue);
				}
				else
					temp.AddChild( Expression());
			}
	
			return temp;
		}
		private static SyntaxTree DefineMatrix(){
		
			SyntaxTree temp;
			temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.FUNCTION,CurrentToken.TokenValue);
			match(CurrentToken.TType,CurrentToken.TokenValue);

			match(TokenType.LPARAN,"(");
			
			int nrow=0;
			temp.AddChild(MatrixRow(ref nrow));
			

			while(CurrentToken.TType == TokenType.LSQUARE)
				temp.AddChild(MatrixRow(ref nrow));
								
			match(TokenType.RPARAN,")");

			return temp;
		}		
		private static SyntaxTree MatrixRow(ref int nrow){
		
			match(TokenType.LSQUARE,"[");
			int tnrow = 0;
			
			SyntaxTree  temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.OTHER,"ROW");
				
			
			if(CurrentToken.TType != TokenType.NUMBER)Error();

			SyntaxTree newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.NUMBER,CurrentToken.TokenValue);
			match(TokenType.NUMBER,CurrentToken.TokenValue);

			if(CurrentToken.TType == TokenType.COLON)
			{
				double st=0,inc=1,ed=0;
				SyntaxTree newtemp2 = 	SyntaxTree.MakeNode(SyntaxTreeNodeType.OTHER,":");
				newtemp2.AddChild(newtemp);
				st = double.Parse(newtemp.NodeValue);
				match(TokenType.COLON,":");
				
				if(CurrentToken.TType != TokenType.NUMBER)Error();
				newtemp = SyntaxTree.MakeNode(SyntaxTreeNodeType.NUMBER,CurrentToken.TokenValue);
				
				match(TokenType.NUMBER,CurrentToken.TokenValue);
				
				if(CurrentToken.TType == TokenType.COLON)
				{
					match(TokenType.COLON,":");
					if(CurrentToken.TType != TokenType.NUMBER)Error();

					newtemp2.AddChild(newtemp);
					inc = double.Parse(newtemp.NodeValue);
					newtemp2.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.NUMBER,CurrentToken.TokenValue));
					ed = double.Parse(CurrentToken.TokenValue);
					match(TokenType.NUMBER,CurrentToken.TokenValue);
				}
				else{
					newtemp2.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.NUMBER,"1"));
					ed = double.Parse(newtemp.NodeValue);
					newtemp2.AddChild(newtemp);
				}
				temp.AddChild(newtemp2);
				tnrow = (int)((ed-st)/inc)+1;
	
			}
			else
			{
				tnrow++;
				temp.AddChild(newtemp);
				while(CurrentToken.TType == TokenType.NUMBER)
				{
					temp.AddChild(SyntaxTree.MakeNode(SyntaxTreeNodeType.NUMBER,CurrentToken.TokenValue));
					match(CurrentToken.TType,CurrentToken.TokenValue);
					tnrow++;
				}
			}
			
			if(tnrow==0)Error();

			if(nrow>0 && tnrow!=nrow)
				Error();
			else
				nrow = tnrow;

			match(TokenType.RSQUARE,"]");

			return temp;
		}

		private static SyntaxTree Identifier(){
			
			SyntaxTree temp = SyntaxTree.MakeNode(SyntaxTreeNodeType.IDENTIFIER,CurrentToken.TokenValue);
			match(TokenType.IDENTIFIER,CurrentToken.TokenValue);

			if(CurrentToken.TType == TokenType.LSQUARE){
				match(TokenType.LSQUARE,"[");
				temp.AddChild(Expression());
				match(TokenType.COMMA,",");
				temp.AddChild(Expression());
				match(TokenType.RSQUARE,"]");
			}
			return temp;
		}
	}
}
