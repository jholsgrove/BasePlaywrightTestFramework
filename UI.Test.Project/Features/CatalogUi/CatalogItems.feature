@Ui
Feature: CatalogItems
	As an adventurer
	I want to see newly added items listed in the catalog
	So that I can purchase helpful items

Scenario: NewItemsAreListedInTheCatalogUi
	Given I create a new catalog item
		| Name  | Price |
		| Bow   | 18    |
	Then the Bow is listed at a price of 18