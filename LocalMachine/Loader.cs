using System;
using System.IO;
using System.Diagnostics;


namespace LocalMachine
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Loader
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//[STAThread]
		public static string apppath;
		static void Main(string[] args)
		{
			
			if(args.Length>0){

				apppath= System.Windows.Forms.Application.StartupPath;
				
				LocalMachine localmachine = new LocalMachine();
				//try
				//{
					localmachine.Initialize();
					localmachine.ProcessFile(args[0]);
				//}
				//catch(Exception e){
				//	Console.WriteLine("Error Occured - {0}", e.Message);				
//					Console.WriteLine(e.StackTrace);	
				//}
				localmachine.Close();
				Console.WriteLine();
				Console.Write("Press enter to exit.");
				Console.ReadLine();
			}
			
			//Console.WriteLine(System.Windows.Forms.Application.StartupPath);
			//Console.ReadLine();
		}
	}
}
