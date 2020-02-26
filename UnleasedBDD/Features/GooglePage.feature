Feature: GooglePage
	In order to search something in Google Page
	As a user
	I want to seacrh value momentum

@regression
Scenario: Search in Google Page
	Given I have searched the details in Google Page
	| searchText			| 
	| Value Momentum  		| 
	Then it should display the results on webpage
	| linkToBeDisplayed									| 
	| ValueMomentum \| IT Services & Software  			| 
