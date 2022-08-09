using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Helper.Project;

namespace UI.Test.Project.Helpers
{
	public class PageBase
	{
		protected readonly PageContext Context;

		public PageBase(PageContext context)
		{
			this.Context = context;
		}

		public async Task WaitForElement(string selector)
		{
			var options = new PageWaitForSelectorOptions
			{
				State = WaitForSelectorState.Visible
			};

			await this.Context.Page.WaitForSelectorAsync(selector, options);
		}

		public async Task WaitForElementToBeHidden(string selector)
		{
			var options = new PageWaitForSelectorOptions
			{
				State = WaitForSelectorState.Hidden
			};

			await this.Context.Page.WaitForSelectorAsync(selector, options);
		}

		public async Task<bool> IsHidden(string selector)
		{
			return await this.Context.Page.IsHiddenAsync(selector);
		}

		public async Task<bool> IsEnabled(string selector)
		{
			return await this.Context.Page.IsEnabledAsync(selector);
		}

		public async Task<IReadOnlyList<string>> GetAllListItems(string selector, string tag)
		{
			return await this.Context.Page.Locator($"{selector} {tag}").AllTextContentsAsync();
		}

		public async Task ClickElement(string selector)
		{
			await this.Context.Page.ClickAsync(selector);
		}

		public async Task Check(string selector)
		{
			await this.Context.Page.CheckAsync(selector);
		}

		public async Task EnterText(string selector, string textToType)
		{
			await this.Context.Page.TypeAsync(selector, textToType);
		}
		public async Task Refresh()
		{
			await this.Context.Page.ReloadAsync();
		}

		public async Task Evaluate(string expression)
		{
			await this.Context.Page.EvaluateAsync(expression);
		}

		public async Task<string> GetDropdownValue(string selector)
		{
			return await this.Context.Page.EvalOnSelectorAsync<string>(selector, EvalActions.SelectDropdownText);
		}

		public async Task SelectDropdownItem(string selector, string valueToSelect)
		{
			await this.Context.Page.SelectOptionAsync(selector, valueToSelect);
		}

		public async Task<string> GetInnerText(string selector)
		{
			return await this.Context.Page.InnerTextAsync(selector);
		}

		public async Task<string> GetElementText(string selector)
		{
			return await this.Context.Page.EvalOnSelectorAsync<string>(selector, EvalActions.GetElementText);
		}

		public async Task<string> GetCssBackgroundColour(string selector)
		{
			return await this.Context.Page.EvalOnSelectorAsync<string>(selector, EvalActions.GetBackgroundColour);
		}

		public async Task<IElementHandle> GetElement(string selector)
		{
			return await this.Context.Page.QuerySelectorAsync(selector);
		}

		public async Task<string> GetElementAttribute(string selector, string attribute)
		{
			return await this.Context.Page.GetAttributeAsync(selector, attribute);
		}

		public async Task<IReadOnlyList<IElementHandle>> GetElements(string selector)
		{
			return await this.Context.Page.QuerySelectorAllAsync(selector);
		}

		public async Task NavigateToUrl(string url)
		{
			await this.Context.Page.GotoAsync(url);
		}
	}
}