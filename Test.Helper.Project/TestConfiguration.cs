using Microsoft.Extensions.Configuration;

namespace Test.Helper.Project
{
	public class TestConfiguration
	{
		private static IConfiguration Config;

		static TestConfiguration()
		{
			Config = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables()
				.Build();
		}

		/// <summary>
		/// Return given configuration item either from Environment Variable, or app.config
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string Get(string name)
		{
			var result = Config.GetValue<string>(name);
			return result;
		}
	}
}