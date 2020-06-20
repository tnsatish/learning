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

		private static log4net.ILog Log1 = log4net.LogManager.GetLogger("SampleWebApplication1.Handler1");

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			string response = "Response " + context.Request.Params["auctionId"] + " " + context.Request.Params["amount"]
				+ context.Request.Params["op"];
			Log.Debug("Got Request: " + response);
			Log1.Debug("Got Request: " + response);
			string json = new StreamReader(context.Request.InputStream).ReadToEnd();
			Log.Debug("Post Body: " + response);
			Log1.Debug("Post Body: " + response);

			Thread t1 = new Thread(new ThreadStart(ThreadProc));
			t1.Start();

			Thread t2 = new Thread(() => ThreadProcWithParams(20000));
			t2.Start();

			Thread t3 = new Thread(() => NonStaticWithParams(20000));
			t3.Start();

			Log.Debug("Let's not wait for the thread to finish");
			Log1.Debug("Let's not wait for the thread to finish");
			context.Response.Write("{\"response\": \"" + json + "\" }");

			
		}

		public static void ThreadProc()
		{
			Thread.Sleep(10000);
			Log.Debug("We are logging in a separate thread");
			Log1.Debug("We are logging in a separate thread");
		}

		public static void ThreadProcWithParams(int value)
		{
			Thread.Sleep(value);
			Log.Debug("We are logging in a separate thread with parameters after waiting for " + value);
			Log1.Debug("We are logging in a separate thread with parameters after waiting for " + value);
		}

		public void NonStaticWithParams(int value)
		{
			Thread.Sleep(value);
			Log.Debug("We are logging in a non static method with parameters after waiting for " + value);
			Log1.Debug("We are logging in a non static method with parameters after waiting for " + value);
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