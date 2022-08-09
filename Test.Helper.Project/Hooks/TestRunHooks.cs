using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System.Net;
using System.Net.Http.Headers;

namespace Test.Helper.Project.Hooks
{
	[Binding]
	public class TestRunHooks
	{
		//public static RestHelper Rest { get; set; }

		public static CookieContainer GlobalCookieContainer = new CookieContainer();
		public static CookieContainer UserCookieContainer = new CookieContainer();
		public static ClientHelper ClientHelper { get; set; }
		public static IHttpClientFactory? TestHttpClientFactory { get; internal set; }

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			LogHelper.Log("[BeforeTestRun] start");
			var killBrowser = TestConfiguration.Get("browser_kill_before_run");

			ClientHelper = new ClientHelper();

			var serviceCollection = new ServiceCollection();
			// Add a named HTTP Clilent we can reuse in order to prevent socket exhaustion
			serviceCollection.AddHttpClient("userClient", c =>
			{
				c.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
				c.DefaultRequestHeaders.Clear();

			}).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
			{
				CookieContainer = UserCookieContainer,
				UseCookies = true,
				AllowAutoRedirect = true
			});

			TestHttpClientFactory = serviceCollection.BuildServiceProvider().GetService<IHttpClientFactory>();

			ClientHelper.TestClient();

			LogHelper.Log("[BeforeTestRun] end");
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			// Perform any necessary teardown actions, *not* using any UI driven libraries.
		}
	}
}
