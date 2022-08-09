using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Test.Helper.Project.DTOs;

namespace Test.Helper.Project.SetupSteps
{
    [Binding]
    public sealed class SetupSteps
    {
        private ScenarioContext ScenarioContext { get; }
        private TestStateContext Context => TestStateContext.Get(ScenarioContext);

        public SetupSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [Given(@"I create a new catalog item")]
        public async Task GivenICreateANewCatalogItem(Table table)
        {
            // Create an object to post from the specflow table
            Context.CatalogItem = table.CreateInstance<CatalogItemDto>();

            // Post it to the endpoint
            var postItem = await RestManager.Post<CatalogItemDto>(Context.CatalogItem);
            
            // Note the outcome for later steps
            Context.LastApiResponse = postItem.TextContent;
            Context.RecordedStatusCode = postItem.StatusCode;

            // Deserialise the object
            Context.PostedCatalogItem = JsonConvert.DeserializeObject<CatalogItemDto>(Context.LastApiResponse);

            // Put a delete call onto the stack to teardown after the scenario using the deserialised object id/ guid
            Context.TeardownActions.Push(async () =>
            {
                await RestManager.Delete<CatalogItemDto>(Context.PostedCatalogItem.Id);
            });
        }
    }
}