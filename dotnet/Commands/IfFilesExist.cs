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

			Console.WriteLine("IfFilesExist " + string.Join(" ", args));

			string[] fileEntries = Directory.GetFiles(args[0]);
			Console.WriteLine("No.of files: " + fileEntries.Length);
			if(fileEntries.Length > 0)
			{
				Console.WriteLine("Files: " + Environment.NewLine + string.Join(Environment.NewLine, fileEntries));
				List<string> parameters = args.ToList();
				parameters.RemoveRange(0, 2);
				Console.WriteLine(args[1]);
				Console.WriteLine(string.Join(" ", parameters));

				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				startInfo.RedirectStandardError = true;
				startInfo.RedirectStandardOutput = true;
				startInfo.FileName = args[1];
				startInfo.UseShellExecute = false;
				startInfo.Arguments = string.Join(" ", parameters);

				System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo = startInfo;
				process.Start();
				Console.WriteLine(process.StandardOutput.ReadToEnd());
				Console.WriteLine(process.StandardError.ReadToEnd());
			}
		}
	}
}
