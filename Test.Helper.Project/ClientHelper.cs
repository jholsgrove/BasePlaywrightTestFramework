using System.Net;
using Test.Helper.Project.Hooks;

namespace Test.Helper.Project
{
	public class ClientHelper
	{
		public static CookieContainer GlobalCookieContainer = new CookieContainer();
		public static string BaseUrl => TestConfiguration.Get("baseUrl");

		public HttpClient TestClient()
		{
			var baseAddress = new Uri(BaseUrl);

			var testClient = TestRunHooks.TestHttpClientFactory.CreateClient("userClient");
			testClient.DefaultRequestHeaders.Clear();
			testClient.BaseAddress = baseAddress;

			LogHelper.Log($"Hit {BaseUrl}");
			testClient.GetAsync(BaseUrl).GetAwaiter().GetResult();

			return testClient;			
		}
	}
}