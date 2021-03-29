using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangedFiles
{
	class Program
	{
		//static void Main(string[] args)
		//{
		//	var appArgs = new AppArguments();
		//	bool validParams = Parser.ParseArgumentsWithUsage(args, appArgs);
		//	if (!validParams)
		//		return;

		//	var filePaths = File.ReadAllLines(appArgs.InputFile).ToList()
		//		.Where(f => f.StartsWith(appArgs.BasePath ?? string.Empty, StringComparison.OrdinalIgnoreCase));

		//	foreach (var filePath in filePaths)
		//	{
		//		var targetFile = string.IsNullOrWhiteSpace(appArgs.BasePath) ? Path.Combine(appArgs.OutputDir, filePath) : filePath.Replace(appArgs.BasePath, appArgs.OutputDir);
		//		Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
		//		File.Copy(filePath, targetFile, true);
		//	}
		//}
	}

	[CommandLineArguments(CaseSensitive = false)]
	public class AppArguments
	{
		[Argument(ArgumentType.Required, HelpText = "Input File", LongName = "InputFile", ShortName = "i")]
		public string InputFile;

		[Argument(ArgumentType.Required, HelpText = "Output Directory", LongName = "OutputDir", ShortName = "o")]
		public string OutputDir;

		[Argument(ArgumentType.AtMostOnce, HelpText = "Base Path", LongName = "BasePath", ShortName = "b")]
		public string BasePath;
	}
}
