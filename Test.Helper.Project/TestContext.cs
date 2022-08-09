using TechTalk.SpecFlow;
using System.Net;
using Test.Helper.Project.DTOs;

namespace Test.Helper.Project
{
	public class TestStateContext
	{
		public string BaseWindow;
		public string LastApiResponse;
		public HttpStatusCode RecordedStatusCode;
		public CatalogItemDto CatalogItem;
		public CatalogItemDto PostedCatalogItem;

		private const string Key = "TreeCallState";

		public static TestStateContext Get(ScenarioContext scenarioContext)
		{
			TestStateContext state;
			lock (scenarioContext)
			{
				if (!scenarioContext.TryGetValue(Key, out state))
				{
					state = new TestStateContext();
					scenarioContext[Key] = state;
				}
			}

			return state;
		}

		private Stack<Action> teardownActions;
		public Stack<Action> TeardownActions => this.teardownActions ?? (this.teardownActions = new Stack<Action>());

		public Guid webhookTestEndpointId;
	}
}