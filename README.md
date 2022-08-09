# BasePlaywrightTestFramework
A framework that uses API manipulation along with GUI checks with Playwright Sharp.

# Install Playwright
Using Powershell 7: cd BasePlaywrightTestFramework\UI.Test.Project\bin\Debug\net6.0> pwsh playwright.ps1 install

# Application under test
This framework performs actions on an in memory API which can be found in this repo [CatalogApi](https://github.com/jholsgrove/CatalogApi). Which is deployed to MS Azure (see the config).

# Omissions
This framework is not to demonstrate the Page Object Model, for a start the GUI is lacking. The focus here is to use API calls to perform common actions in the test suite, like Setup and Teardown.