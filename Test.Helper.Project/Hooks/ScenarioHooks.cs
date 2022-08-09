using TechTalk.SpecFlow;
using System.Net;
using BoDi;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Test.Helper.Project.Hooks
{
	[Binding]
	public class ScenarioHooks : Steps
	{
		public static CookieContainer GlobalCookieContainer = new CookieContainer();
		public static CookieContainer UserCookieContainer = new CookieContainer();
		private readonly IObjectContainer ObjectContainer;
		private IPlaywright PlaywrightDriver { get; set; }
		private IBrowser Browser { get; set; }
		private IBrowserContext BrowserContext { get; set; }
		public static IHttpClientFactory? TestHttpClientFactory { get; internal set; }

		public ScenarioHooks(IObjectContainer objectContainer)
		{
			ObjectContainer = objectContainer;
		}

		[BeforeScenario]
		public async Task BeforeScenario()
		{
			LogHelper.Log("[BeforeScenario] start");

			var browserToUse = TestConfiguration.Get("Browser");
			var url = TestConfiguration.Get("BaseUrl");
			var method = TestConfiguration.Get("TestMethod");
			var headless = TestConfiguration.Get("Headless");

			if (method.ToLower() != "api")
			{
				PlaywrightDriver = await Playwright.CreateAsync();
				
				switch (browserToUse.ToLower())
				{
					case "chrome":
						Browser = await PlaywrightDriver.Chromium.LaunchAsync(new() { Headless = bool.Parse(headless) });
						break;
					case "firefox":
						Browser = await PlaywrightDriver.Firefox.LaunchAsync(new() { Headless = bool.Parse(headless) });
						break;
					case "webkit":
						Browser = await PlaywrightDriver.Webkit.LaunchAsync(new() { Headless = bool.Parse(headless) });
						break;
				}

				BrowserContext = await Browser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true, IgnoreHTTPSErrors = true });
				var page = await BrowserContext.NewPageAsync();

				await BrowserContext.Tracing.StartAsync(new TracingStartOptions
				{
					Name = "Trace",
					Screenshots = true,
					Snapshots = true
				});

				await page.SetViewportSizeAsync(width: 2560, height: 1440);

				ObjectContainer.RegisterInstanceAs(PlaywrightDriver);
				ObjectContainer.RegisterInstanceAs(page);

				await page.GotoAsync(url);
			}

			LogHelper.Log("[BeforeScenario] end");
		}

		[AfterScenario]
		public async Task AfterScenario()
		{
			var method = TestConfiguration.Get("TestMethod");

			LogHelper.Log("[AfterScenario] start");
			if (method.ToLower() != "api")
			{

				if (ScenarioContext.TestError != null)
				{
					LogHelper.Log($"Test Failed: {ScenarioContext.TestError.Message}");
					await BrowserContext.Tracing.StopAsync(new TracingStopOptions
					{
						Path = $"..\\..\\..\\Traces\\{TestContext.CurrentContext.Test.Name}-trace.zip"
					});
				}

				await Browser.CloseAsync();
				PlaywrightDriver.Dispose();
			}

			LogHelper.Log("[AfterScenario] performing teardown");
			ConductTeardown();

			LogHelper.Log("[AfterScenario] end");
		}

		private void ConductTeardown()
        {
			// Now iterate on things in the stack
			var actionStack = TestStateContext.Get(ScenarioContext).TeardownActions;

			while (actionStack.Count > 0)
			{
				var nextAction = actionStack.Pop();
				nextAction();
			}
		}
	}
}