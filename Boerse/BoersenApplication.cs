using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boerse
{
	class BoersenApplication
	{
		static void Main(string[] args)
		{
			Debug.WriteLine("Started!");
			using (var server = new RestServer())
			{
				server.LogToConsole().Start();
				Console.ReadLine();
				server.Stop();
			}
		}
	}

	[RestResource]
	public class TestResource
	{
		[RestRoute(HttpMethod = Grapevine.Shared.HttpMethod.GET, PathInfo = "/repeat")]
		public IHttpContext RepeatMe(IHttpContext context)
		{
			var word = context.Request.QueryString["word"] ?? "what?";
			context.Response.SendResponse(word);
			return context;
		}

		[RestRoute]
		public IHttpContext HelloWord(IHttpContext context)
		{
			context.Response.SendResponse("Hello World!");
			return context;
		}
	}
}
