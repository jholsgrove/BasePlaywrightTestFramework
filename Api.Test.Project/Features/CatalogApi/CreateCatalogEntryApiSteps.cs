using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;
using Test.Helper.Project;
using Test.Helper.Project.DTOs;

namespace Api.Test.Project.Features.CatalogApi
{
    [Binding]
    internal sealed class CreateCatalogEntryApiSteps
    {
        private ScenarioContext ScenarioContext { get; }
        private TestStateContext Context => TestStateContext.Get(ScenarioContext);

        public CreateCatalogEntryApiSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [Then(@"the item gets created")]
        public void ThenTheItemGetsCreated()
        {
            var postedCatalogItem = JsonConvert.DeserializeObject<CatalogItemDto>(Context.LastApiResponse);

            Assert.That(Context.RecordedStatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(postedCatalogItem.Name, Is.EqualTo(Context.CatalogItem.Name));
            Assert.That(postedCatalogItem.Price, Is.EqualTo(Context.CatalogItem.Price));
        }
    }
}