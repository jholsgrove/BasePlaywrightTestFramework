@Api
Feature: CreateCatalogEntry
	As an adventurer
	I want certain items available to me
	So that I can complete quests

Scenario: CreateCatalogItem
	Given I create a new catalog item
		| Name  | Price |
		| Boots | 15    |
	Then the item gets created