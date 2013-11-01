using System;

namespace ASMMath
{
	public class Matrix
	{
		public double[,] m_MatArr;
		private int m_Rows,m_Cols;
		
        //****************************Constructors************************		
		public Matrix()
		{
			m_MatArr = new double[2,2];
			m_Rows = 2;
			m_Cols = 2;
		}
		public Matrix(int rows,int cols)
		{
			if(rows>0 && cols>0)
			{
				m_MatArr = new double[rows,cols];
				m_Rows = rows;
				m_Cols = cols;
			}
			else{
				throw new Exception("Rows and Columns must be greater than 0.");
			}
		}
		public Matrix(int rowscols)
		{
			if(rowscols>0)
			{
				m_MatArr = new double[rowscols,rowscols];
				m_Rows = rowscols;
				m_Cols = rowscols;
			}
			else
			{
				throw new Exception("Rows and Columns must be greater than 0.");
			}
		}
		//*****************************************************************
		//*************************Properties******************************
		public int Rows{
			get{
				return m_Rows;
			}
		}
		public int Columns
		{
			get
			{
				return m_Cols;
			}
		}
		public int Size{
			get
			{
				return m_Cols * m_Rows;
			}
		}
		public bool SquareMatrix{
			get
			{
				if (Rows == Columns)return true;
				return false;
			}
		}
		//****************************************************************
		//****************************Methods*****************************
		public void Initialize(){
			Initialize(0);
		}
		public void Initialize(double iValue){
			for (int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					m_MatArr[rw,cl] = iValue;
		}
		
		public void SetValue(int row,int column,double Value){
			if (row>0 && row<=Rows && column>0 && column<=Columns)
			{
				m_MatArr[row-1,column-1]= Value;
			}
			else{
				throw new Exception("Index out of bounds."); 
			}
		}
		public double GetValue(int row,int column)
		{
			if (row>0 && row<=Rows && column>0 && column<=Columns)
			{
				return m_MatArr[row-1,column-1];
			}
			else
			{
				throw new Exception("Index out of bounds.");
			}
		}

		public void Transpose(){
			
			double[,] tempArr = new double[Columns,Rows];
			
			for(int rw=0;rw<Columns;rw++)
				for(int cl=0;cl<Rows;cl++)
					tempArr[rw,cl] = m_MatArr[cl,rw];

			m_MatArr = tempArr;
			
			int temp = m_Rows;
			m_Rows = m_Cols;
			m_Cols = temp;
			
		}
		public Matrix ReturnTranspose(){
			
			Matrix tempMat = new Matrix(Rows,Columns);
			tempMat.CopyMatrix(this);
			tempMat.Transpose();
			return tempMat;
		}

		public void CopyMatrix(Matrix sMat){
			if(Rows == sMat.Rows && Columns == sMat.Columns)
			{
				for(int rw=0;rw<Rows;rw++)
					for(int cl=0;cl<Columns;cl++)
						m_MatArr[rw,cl] = sMat.m_MatArr[rw,cl];
			}
			else
			{
				throw new Exception ("Number of Rows and Columns must be equal.");
			}
		}

		public Matrix ReturnClone()
		{
			Matrix temp = new Matrix(this.Rows,this.Columns);
			temp.CopyMatrix(this);
			return temp;
		}
		public Matrix ReturnNegative(){
			
			Matrix temp = new Matrix(this.Rows,this.Columns);
			
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = -m_MatArr[rw,cl];
			
			return temp;
		}
		public Matrix ReturnSquareRoot()
		{
			
			Matrix temp = new Matrix(this.Rows,this.Columns);
			
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Sqrt(m_MatArr[rw,cl]);
			
			return temp;
		}
		public void SquareRoot()
		{
			
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					m_MatArr[rw,cl] = Math.Sqrt(m_MatArr[rw,cl]);

		}

		public Matrix EliminateRC(int row,int col)
		{
			if (Rows > 1 && Columns>1 && row>0 && row<=Rows	&& col>0 && col<=Columns)
			{
				Matrix temp = new Matrix(Rows-1,Columns-1);
				row--;col--;
				int rwt=0,clt;
				for(int rw=0;rw<Rows;rw++)
					if(rw!=row)
					{
						clt=0;
						for(int cl=0;cl<Columns;cl++)
							if(cl!=col)
							{
								temp.m_MatArr[rwt,clt] = m_MatArr[rw,cl];
								clt++;
							}
						rwt++;
					}
				return temp;
			}
			else{
				return this;
			}
		}
		public Matrix ReturnSin(){
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Sin(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnCos()
		{
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Cos(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnTan()
		{
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Tan(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnArcSin()
		{
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Asin(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnArcCos()
		{
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Acos(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnArcTan()
		{
			Matrix temp =new Matrix(Rows,Columns);
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					temp.m_MatArr[rw,cl] = Math.Atan(this.m_MatArr[rw,cl]);
			return temp;
		}
		public Matrix ReturnAdjoint(){
			if(SquareMatrix == true)
			{
				if(Columns>1)
				{
					Matrix temp =new Matrix(Rows,Columns);
					for(int rw=0;rw<Rows;rw++)
						for(int cl=0;cl<Columns;cl++)
						{
							Matrix temp2=EliminateRC(rw+1,cl+1);
							temp.m_MatArr[rw,cl] = Math.Pow(-1,rw+cl) * FindDeterminant(temp2);
						}
					temp.Transpose();
					return temp;
				}
				else{
					return this.ReturnClone();
				}
			}
			else{
				throw new Exception("Only applicable to square Matrix.");
			}
		}
		public void Cos()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Cos(this.m_MatArr[rw,cl]);
		}
		public void Sin()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Sin(this.m_MatArr[rw,cl]);
		}
		public void Tan()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Tan(this.m_MatArr[rw,cl]);
		}
		public void ArcCos()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Acos(this.m_MatArr[rw,cl]);
		}
		public void ArcSin()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Asin(this.m_MatArr[rw,cl]);
		}
		public void ArcTan()
		{
			for(int rw=0;rw<Rows;rw++)
				for(int cl=0;cl<Columns;cl++)
					this.m_MatArr[rw,cl] = Math.Atan(this.m_MatArr[rw,cl]);
		}
		public string Display(){
			
			string temp="\r\n";
			
			for(int rw=0;rw<Rows;rw++){
				for(int cl=0;cl<Columns;cl++)
					temp = temp + m_MatArr[rw,cl] + "   ";
				temp += "\r\n";
			}
			return temp;	
		}
		public static Matrix NewIdentityMatrix(int rowcols){
			Matrix temp = new Matrix(rowcols);
			for(int a=0;a<rowcols;a++)
				temp.m_MatArr[a,a] = 1;
			return temp;
		}
		public static double FindDeterminant(Matrix Mat)
		{
			if (Mat.SquareMatrix == true)
			{
				if (Mat.Columns==1)
				{
					return Mat.m_MatArr[0,0];
				}
				else
				{
					double tvar=0;
					for(int cl=0;cl<Mat.Columns;cl++){
						Matrix temp = Mat.EliminateRC(1,cl+1);
						tvar+=(Math.Pow(-1,cl)*Mat.m_MatArr[0,cl]*FindDeterminant(temp));
					}
					return tvar;
				}
			}
			else
			{
				Console.Error.WriteLine("Only applicable to square Matrix.");
				return 0;
			}
		}

		public byte[] GetBytes(){
			byte[] temp = new byte[Buffer.ByteLength(m_MatArr)];
			Buffer.BlockCopy(m_MatArr,0,temp,0,Buffer.ByteLength(m_MatArr));
			return temp;
		}
		
		public void SetBytes(byte[] bytedata){
			Buffer.BlockCopy(bytedata,0,m_MatArr,0,Buffer.ByteLength(m_MatArr));
		}

		public Matrix ReturnInvExp(double Value){
			Matrix temp = new Matrix(this.Rows,this.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = System.Math.Pow(Value,this.m_MatArr[rw,cl]);
			return temp;
		}
		public void InvExp(double Value)
		{
			for(int rw=0;rw<this.Rows;rw++)
				for(int cl=0;cl<this.Columns;cl++)
					this.m_MatArr[rw,cl] = System.Math.Pow(Value,this.m_MatArr[rw,cl]);
		}
		public Matrix ReturnInvMod(double Value)
		{
			Matrix temp = new Matrix(this.Rows,this.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = Value % this.m_MatArr[rw,cl];
			return temp;
		}
		public void InvMod(double Value)
		{
			for(int rw=0;rw<this.Rows;rw++)
				for(int cl=0;cl<this.Columns;cl++)
					this.m_MatArr[rw,cl] = Value % this.m_MatArr[rw,cl];
		}
		public Matrix ReturnInvSub(double Value)
		{
			Matrix temp = new Matrix(this.Rows,this.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = Value - this.m_MatArr[rw,cl];
			return temp;
		}
		public void InvSub(double Value)
		{
			for(int rw=0;rw<this.Rows;rw++)
				for(int cl=0;cl<this.Columns;cl++)
					this.m_MatArr[rw,cl] = Value - this.m_MatArr[rw,cl];
		}
		public Matrix ReturnInvDiv(double Value)
		{
			Matrix temp = new Matrix(this.Rows,this.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = Value / this.m_MatArr[rw,cl];
			return temp;
		}
		public void InvDiv(double Value)
		{
			for(int rw=0;rw<this.Rows;rw++)
				for(int cl=0;cl<this.Columns;cl++)
					this.m_MatArr[rw,cl] = Value / this.m_MatArr[rw,cl];
		}
		//****************************************************************
		//******************************Overloaded Operators*********************
		public static Matrix operator+(Matrix lMat,Matrix rMat)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			if(lMat.Rows == rMat.Rows && lMat.Columns == rMat.Columns)
			{
					for(int rw=0;rw<temp.Rows;rw++)
						for(int cl=0;cl<temp.Columns;cl++)
							temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] + rMat.m_MatArr[rw,cl];
			}
			else{
				throw new Exception("Two matrices should be equal in order to perform the operation.");
			}
			return temp;
		}
		public static Matrix operator+(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] + Value;
			return temp;
		}
		
		public static Matrix operator-(Matrix lMat,Matrix rMat)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			if(lMat.Rows == rMat.Rows && lMat.Columns == rMat.Columns)
			{
				for(int rw=0;rw<temp.Rows;rw++)
					for(int cl=0;cl<temp.Columns;cl++)
						temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] - rMat.m_MatArr[rw,cl];
			}
			else
			{
				throw new Exception("Two matrices should be equal in order to perform the operation.");
			}
			return temp;
		}
		public static Matrix operator-(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] - Value;
			return temp;
		}
		
		public static Matrix operator*(Matrix lMat,Matrix rMat)
		{
			if(lMat.Columns == rMat.Rows)
			{
				Matrix temp = new Matrix(lMat.Rows,rMat.Columns);
				temp.Initialize();
				for(int rw=0;rw<temp.Rows;rw++)
					for(int cl=0;cl<temp.Columns;cl++)
						for(int cl2=0;cl2<lMat.Columns;cl2++)
							temp.m_MatArr[rw,cl] =temp.m_MatArr[rw,cl] + lMat.m_MatArr[rw,cl2] * rMat.m_MatArr[cl2,cl];
							
				return temp;
			}
			else
			{
				throw new Exception("Number of Columns of Left Matrix must be equal to Rows of Right Matrix.");
			}
		}
		public static Matrix operator*(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] * Value;
			return temp;
		}
		public static Matrix operator/(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] / Value;
			return temp;
		}
		public static Matrix operator/(Matrix lMat,Matrix rMat)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			if(lMat.Rows == rMat.Rows && lMat.Columns == rMat.Columns)
			{
				for(int rw=0;rw<temp.Rows;rw++)
					for(int cl=0;cl<temp.Columns;cl++)
						temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] / rMat.m_MatArr[rw,cl];
			}
			else
			{
				throw new Exception("Two matrices should be equal in order to perform the operation.");
			}
			return temp;
		}
		public static Matrix operator^(Matrix lMat,Matrix rMat)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			if(lMat.Rows == rMat.Rows && lMat.Columns == rMat.Columns)
			{
				for(int rw=0;rw<temp.Rows;rw++)
					for(int cl=0;cl<temp.Columns;cl++)
						temp.m_MatArr[rw,cl] = System.Math.Pow(lMat.m_MatArr[rw,cl],rMat.m_MatArr[rw,cl]);
			}
			else
			{
				throw new Exception("Two matrices should be equal in order to perform the operation.");
			}
			return temp;
		}
		public static Matrix operator^(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = System.Math.Pow(lMat.m_MatArr[rw,cl],Value);
			return temp;
		}
		public static Matrix operator%(Matrix lMat,Matrix rMat)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			if(lMat.Rows == rMat.Rows && lMat.Columns == rMat.Columns)
			{
				for(int rw=0;rw<temp.Rows;rw++)
					for(int cl=0;cl<temp.Columns;cl++)
						temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] % rMat.m_MatArr[rw,cl];
			}
			else
			{
				throw new Exception("Two matrices should be equal in order to perform the operation.");
			}
			return temp;
		}
		public static Matrix operator%(Matrix lMat,double Value)
		{
			Matrix temp = new Matrix(lMat.Rows,lMat.Columns);
			for(int rw=0;rw<temp.Rows;rw++)
				for(int cl=0;cl<temp.Columns;cl++)
					temp.m_MatArr[rw,cl] = lMat.m_MatArr[rw,cl] % Value;
			return temp;
		}
		public static bool operator ==(Matrix lMat,Matrix rMat){
			if(lMat.Rows != rMat.Rows || lMat.Columns != rMat.Columns)return false;
			
				for(int rw=0;rw<rMat.Rows;rw++)
					for(int cl=0;cl<rMat.Columns;cl++)
						if(lMat.m_MatArr[rw,cl]!= rMat.m_MatArr[rw,cl])return false;
			return true;
		}
		public static bool operator !=(Matrix lMat,Matrix rMat)
		{
			return !(lMat==rMat);
		}
		public static Matrix operator++(Matrix Mat)
		{
			for(int rw=0;rw<Mat.Rows;rw++)
					for(int cl=0;cl<Mat.Columns;cl++)
						Mat.m_MatArr[rw,cl]++;
			return Mat;
		}
		public static Matrix operator--(Matrix Mat)
		{
			for(int rw=0;rw<Mat.Rows;rw++)
				for(int cl=0;cl<Mat.Columns;cl++)
					Mat.m_MatArr[rw,cl]--;
			return Mat;
		}
		
		//****************************************************************
	}
}
