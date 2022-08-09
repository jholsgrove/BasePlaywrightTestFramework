using Newtonsoft.Json;
using System.Net;
using System.Text;
using Test.Helper.Project.Hooks;

namespace Test.Helper.Project
{

		public class StandardResponse<T>
		{
			public T Response => Deserialize();
			public HttpStatusCode StatusCode { get; set; }
			public bool IsSuccess { get; set; }
			public string TextContent { get; set; }
			private readonly string content;
			public StandardResponse(string content)
			{
				this.content = content;
			}

			private T Deserialize()
			{
				return JsonConvert.DeserializeObject<T>(this.content);
			}
		}

	public class RestManager
	{
		public static async Task<StandardResponse<T>> Post<T>(object body, bool jsonifyContent = true)
		{
			var baseUrl = TestConfiguration.Get("baseUrl");
			var client = TestRunHooks.TestHttpClientFactory.CreateClient("userClient");

			var webRequest = new HttpRequestMessage(HttpMethod.Post, baseUrl);
			{
				webRequest.Content = jsonifyContent ? new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json") : (HttpContent)body;
			}

			var send = await client.SendAsync(webRequest);
			var response = new StandardResponse<T>(send.Content.ReadAsStringAsync().Result);
			response.StatusCode = send.StatusCode;
			response.TextContent = send.Content.ReadAsStringAsync().Result;

			return response;
		}

		public static async Task<StandardResponse<T>> Delete<T>(string id)
		{
			var baseUrl = TestConfiguration.Get("baseUrl");
			var client = TestRunHooks.TestHttpClientFactory.CreateClient("userClient");

			var webRequest = new HttpRequestMessage(HttpMethod.Delete, $"{baseUrl}/{id}");

			var send = await client.SendAsync(webRequest);
			var response = new StandardResponse<T>(send.Content.ReadAsStringAsync().Result);
			response.StatusCode = send.StatusCode;
			response.TextContent = send.Content.ReadAsStringAsync().Result;

			return response;
		}
	}
}