using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Configuration;

namespace ConsoleApp
{
	public class MSMQ
	{
		public static void test()
		{
			string queueName = ConfigurationManager.AppSettings["MSMQ"];
			string label = "AgentSearch";
			string body = "Test Body";

			SendMessage(queueName, label, body);
		}

		public static void SendMessage(String formatName, String label, String body)
		{
			using (MessageQueue queue = new MessageQueue())
			using (System.Messaging.Message message = new System.Messaging.Message())
			{
				InternalSendMessage(queue, message, formatName, label, body, true);
			}
		}

		private static void InternalSendMessage(MessageQueue queue, Message message, string formatName, string label, string body, bool isTransactional)
		{
			Console.WriteLine(formatName);
			Console.WriteLine(label);
			Console.WriteLine(body);
			try
			{
				queue.Path = "FormatName:" + formatName;
				message.Label = label;
				message.Body = body;
				message.Recoverable = true; // Make the messages survive a reboot.

				if (isTransactional)
				{
					queue.Send(message, MessageQueueTransactionType.Single);
					Console.WriteLine("Sent transactional message");
				}
				else
				{
					queue.Send(message);
					Console.WriteLine("Sent non transactional message");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);
			}
		}

	}
}
