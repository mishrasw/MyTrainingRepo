Feature: Create Product
	In order to create a new product
	As a unleased user
	I want to input proper product data
	And save the product details

Background: 
Given User logs in to Unleased 

@regression
Scenario: Create a new product
	Given I have navigated to product screen and entered product details
	| ProductCode	| ProductDescription |
	| TST6			| Test Comment	 |
	Then I verify the product is created successfully
	| ProductCode	| 
	| TST6			| 
	