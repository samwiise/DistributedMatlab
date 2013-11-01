using System;
using System.Collections;

namespace DMatlab
{
	/// <summary>
	/// Summary description for GlobalModule.
	/// </summary>
	

	public class NumberAndIdentifierTable{
	
		public ArrayList identifiers = new ArrayList();
		public ArrayList numbers = new ArrayList();
		public ArrayList Literals = new ArrayList();
	
		public ushort GetLocation(string identifier){
			int a=0;
            for(a=0;a<identifiers.Count;a++)
				if(identifiers[a].ToString().Equals(identifier))
					return (ushort)a;
			identifiers.Add(identifier);
			return (ushort)a;
		}
		public ushort GetLocation(double num)
		{
			int a=0;
			for(a=0;a<numbers.Count;a++)
				if(double.Parse(numbers[a].ToString())==num)
					return (ushort)a;
			numbers.Add(num);
			return (ushort)a;
		}
		public ushort GetLiteralLocation(string literal)
		{
			int a=0;
			for(a=0;a<Literals.Count;a++)
				if(Literals[a].ToString().Equals(literal))
					return (ushort)a;
			Literals.Add(literal);
			return (ushort)a;
		}

		public double[] GetNumbers(){
			double[] nums = new double[numbers.Count];
			for(int a=0;a<numbers.Count;a++)
				nums[a]=double.Parse(numbers[a].ToString());
			return nums;
		}
		public string[] GetLiterals()
		{
			string[] strs = new string[Literals.Count];
			for(int a=0;a<Literals.Count;a++)
				strs[a]= Literals[a].ToString();
			return strs;
		}
	}


}
