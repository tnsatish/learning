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

			File.WriteAllLines("c:\\temp\\testfile-" + DateTime.Now.Ticks + ".txt", args);
			Console.WriteLine("IfFilesExist " + args[0] + " " + args[1]);

			string[] fileEntries = Directory.GetFiles(args[0]);
			Console.WriteLine("No.of files: " + fileEntries.Length);
			if(fileEntries.Length > 0)
			{
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

				//var process = System.Diagnostics.Process.Start(args[1], string.Join(" ", parameters));
				WriteToFile(process.StandardOutput, "info");
				WriteToFile(process.StandardError, "error");
			}
		}

		private static void WriteToFile(StreamReader reader, string fileName)
		{
			if(reader.Peek() > 0)
			{
				using (StreamWriter sw = new StreamWriter("c:\\temp\\testfile-" + DateTime.Now.Ticks + "-" + fileName + ".txt"))
				{
					char[] buffer = new char[1024];
					int index = 0;
					int count = 0;
					while ((count = reader.Read(buffer, index, 1024)) > 0)
					{
						List<char> readText = new List<char>(buffer);
						readText.RemoveRange(count, readText.Count - count);
						sw.Write(readText.ToArray());
						//index += count;
					}
				}
			}
		}
	}
}
