using NUnit.Framework;
using TechTalk.SpecFlow;
using Test.Helper.Project;
using System.Threading.Tasks;
using UI.Test.Project.Helpers;

namespace UI.Test.Project.Features.CatalogUi
{
    [Binding]
    internal class CatalogItemsSteps : PageBase
    {
        public CatalogItemsSteps(PageContext context) : base(context)
        {

        }

        [Then(@"the (.*) is listed at a price of (.*)")]
        public async Task ThenTheItemIsListedAtAPriceOfX(string itemName, string priceOfItem)
        {
            // Refresh, the browser navigated on the scenario hook and THEN a new item was posted.
            await Refresh();

            // Get the element on the page by the tag PRE
            var content = await GetElementText("body > pre");

            // Check we have a Bow priced at 18
            Assert.That(content.Contains($"\"name\":\"{itemName}\",\"price\":{priceOfItem}"));
        }
    }
}