using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace SampleWebApplication1
{
	/// <summary>
	/// Summary description for Handler1
	/// </summary>
	public class Handler1 : IHttpHandler
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			string response = "Response " + context.Request.Params["auctionId"] + " " + context.Request.Params["amount"]
				+ context.Request.Params["op"];
			Log.Debug("Got Request: " + response);
			string json = new StreamReader(context.Request.InputStream).ReadToEnd();
			Log.Debug("Post Body: " + response);

			Thread t = new Thread(new ThreadStart(ThreadProc));
			t.Start();

			Log.Debug("Let's not wait for the thread to finish");

			context.Response.Write("{\"response\": \"" + json + "\" }");
		}

		public static void ThreadProc()
		{
			Thread.Sleep(10000);
			Log.Debug("We are logging in a separate thread");
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}