using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangedFiles
{
	public class IfFilesExist
	{
		public static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				Console.WriteLine("Usage: IfFilesExist <Directory> <Command> [Parameters...]\n");
				Console.WriteLine("Directory:       Checks whether files exist in this folder or not.");
				Console.WriteLine("Command:         Command to be executed.");
				Console.WriteLine("Parameters:      Parameters for the command.");
				return;
			}

			string[] fileEntries = Directory.GetFiles(args[0]);
			if(fileEntries.Length > 0)
			{
				List<string> parameters = args.ToList();
				parameters.RemoveRange(0, 2);
				System.Diagnostics.Process.Start(args[1], string.Join(" ", parameters));
			}
		}
	}
}
