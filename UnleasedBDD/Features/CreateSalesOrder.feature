Feature: Create Sales Order
	In order to create a sales order
	As a unleased user
	I want to create a sales order from saled order module

Background: 
Given User logs in to Unleased 

@regression
Scenario: Create a new sales order
	Given I have created a new sales order
	| CustomerCode | ProductCode | Quantity |
	| GBRO         | COUCH3      | 1        |
	And I am able to complete a sales Order
	Then verify stocks in hand for the product
	| ProductCode |
	| COUCH3      |
	
	