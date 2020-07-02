using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirWGet
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length != 1)
			{
				Console.WriteLine("Usage: DirWGet <URL>");
				return;
			}
			string url = args[0];
			Console.WriteLine("URL: " + url);
			if(!url.StartsWith("http"))
			{
				Console.WriteLine("Invalid URL: " + url);
				return;
			}
			string removeDomain = url.Substring(url.IndexOf('/', 9)+1);
			Console.WriteLine("After removing domain: " + removeDomain);
			string removeQueryParams = removeDomain;
			if(removeQueryParams.IndexOf('?') > 0)
			{
				removeQueryParams = removeQueryParams.Substring(0, removeQueryParams.IndexOf('?'));
			}
			Console.WriteLine("After removing Query params: " + removeQueryParams);
			string removeFile = removeQueryParams.Substring(0, removeQueryParams.LastIndexOf("/"));
			Console.WriteLine("After removing file: " + removeFile);
			Directory.CreateDirectory(removeFile);

			//string cmd = "wget " + url + " -O " + removeQueryParams;
			System.Diagnostics.Process.Start("WGET.exe", url + " -O " + removeQueryParams);
		}
	}
}
