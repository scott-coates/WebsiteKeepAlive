using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace WebsiteKeepAlive
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Missing URL");
				return 1;
			}

			HttpWebRequest request;

			try
			{
				request = (HttpWebRequest) WebRequest.Create(args[0]);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 4;
			}

			request.AllowAutoRedirect = true;

			if(args.Length > 1)
			{
				request.Credentials = new NetworkCredential(args[1], args[2]);
			}
			else
			{
				request.UseDefaultCredentials = true;
			}
			
			try
			{
				using (var response = (HttpWebResponse)request.GetResponse())
				{
					if (response.StatusCode.ToString() != "OK")
					{
						Console.WriteLine("Error: " + response.StatusCode);
						return 3;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 2;
			}

			Console.WriteLine("OK");

			return 0;
		}
	}
}
