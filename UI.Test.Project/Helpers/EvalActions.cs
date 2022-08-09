namespace UI.Test.Project.Helpers
{
	internal class EvalActions
	{
		public const string GetElementText = "(e, value) => e.textContent";
		public const string GetBackgroundColour = "element => getComputedStyle(element).backgroundColor";
		public const string SelectDropdownValue = "el => el.value";
		public const string SelectDropdownText = "sel => sel.options[sel.options.selectedIndex].textContent";
	}
}