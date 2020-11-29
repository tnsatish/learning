using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangedFiles
{
	public class CopyFiles
	{
		public static void Main(string[] args)
		{
			if(args.Length < 2)
			{
				Console.WriteLine("Usage: CopyFiles <InputFile> <OutputDirectory> [BasePath]\n");
				Console.WriteLine("InputFile:       The file that contains the list of files to be copied.");
				Console.WriteLine("OutputDirectory: Target Directory to copy all the files.");
				Console.WriteLine("BasePath:        If BasePath is passed, it would copy only the files from this directory to the output directory.");
				Console.WriteLine("                 It won't copy the directory strucutre of the base path in the target directory.");
				return;
			}

			var basePath = args.Length >= 3 ? args[2] : string.Empty;
			basePath = basePath.Replace("\\", "/");
			var targetDir = args[1];
			if (!targetDir.EndsWith("\\"))
				targetDir += "\\";
			var filePaths = File.ReadAllLines(args[0]).ToList()
				.Where(f => f.StartsWith(basePath ?? string.Empty, StringComparison.OrdinalIgnoreCase));

			foreach (var filePath in filePaths)
			{
				var targetFile = string.IsNullOrWhiteSpace(basePath) ? Path.Combine(targetDir, filePath) : filePath.Replace(basePath, targetDir);
				Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
				File.Copy(filePath, targetFile, true);
			}
		}
	}
}
