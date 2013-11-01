using System;

namespace DMatlab
{

	public enum SyntaxTreeNodeType{
		IDENTIFIER,NUMBER,OPERATOR,FUNCTION,OTHER,BLOCK,STRUCTURE,REFERENCE,PROCEDURE,LITERAL
	}

	public class SyntaxTree
	{

		public SyntaxTreeNodeType NodeType;
		public string NodeValue;
		public System.Collections.ArrayList ChildNodes;
		public SyntaxTree (){
			ChildNodes = new System.Collections.ArrayList();
		}		
		public static SyntaxTree MakeNode(SyntaxTreeNodeType NodeType,string NodeValue){
			SyntaxTree temp = new SyntaxTree();
			temp.NodeType = NodeType;
			temp.NodeValue = NodeValue;
			return temp;
		}

		public void AddChild(SyntaxTree ChildNode){
			ChildNodes.Add(ChildNode);		
		}
		public SyntaxTree ReturnClone(){
			SyntaxTree temp = new SyntaxTree();
			temp.NodeValue = this.NodeValue;
			temp.NodeType = this.NodeType ;

			foreach(SyntaxTree ntmp in this.ChildNodes)
				temp.ChildNodes.Add(ntmp.ReturnClone());

			return temp;
			
		}
	}
}
