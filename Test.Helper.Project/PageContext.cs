using Microsoft.Playwright;

namespace Test.Helper.Project
{
	public class PageContext
	{
		public IPage Page { get; set; }

		public PageContext(IPage page)
		{
			Page = page;
		}
	}
}