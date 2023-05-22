@fab
Feature: Fabricante

A short summary of the feature

Scenario: Test the Fabricante GET operation in an empty DB
	Given an empty Fabricante table
	When I make a Fabricante GET request
	Then I receive a 204 status code from Fabricante request

Scenario: Test the Fabricante GET operation in a populated DB
	Given a table populated by Fabricantes
	When I make a Fabricante GET request
	Then I receive a 200 status code with the following Fabricante List:
		| id | nome   | 
		| 1  | Dummy1 |
		| 2  | Dummy2 |
		| 3  | Dummy3 |

Scenario: Test the Fabricante POST operation
	Given a table populated by Fabricantes
	When I make a POST request for a Fabricante named "Dummy4":
	Then I receive a 200 status code from Fabricante request
	When I GET the Fabricante data back form the DB
	Then I receive a 200 status code from Fabricante request
	And there contains a Fabricante named "Dummy4"

Scenario: Test the Fabricante POST operation with a null object
	Given a table populated by Fabricantes
	When I make a Fabricante POST request using a null JSON
	Then I receive a 204 status code from Fabricante request

Scenario: Test the Fabricante POST operation with incomplete info
	Given a table populated by Fabricantes
	When I make a Fabricante POST request using the following JSON:
	| id   | nome |
	|      |      |
	Then I receive a 400 status code from Fabricante request

Scenario: Test the Fabricante PUT operation
	Given a table populated by Fabricantes
	When I make a PUT request for a Fabricante of id 3, naming it "Dummy0":
	Then I receive a 200 status code from Fabricante request
	When I GET the Fabricante data back form the DB
	Then I receive a 200 status code from Fabricante request
	And the Fabricante of id 3 is now named "Dummy0"

Scenario: Test the Fabricante PUT operation with a null object
	Given a table populated by Fabricantes
	When I make a Fabricante PUT request using a null JSON
	Then I receive a 204 status code from Fabricante request

Scenario: Test the Fabricante PUT operation with incomplete info
	Given a table populated by Fabricantes
	When I make a Fabricante PUT request using the following JSON:
	| id   | nome |
	|      |      |
	Then I receive a 400 status code from Fabricante request

Scenario: Test the Fabricante PUT operation for a non-existent element
	Given a table populated by Fabricantes
	When I make a Fabricante PUT request using the following JSON:
	| id | nome   |
	| 5  | Dummy0 |
	Then I receive a 404 status code from Fabricante request

Scenario: Test the Fabricante DELETE operation
	Given a table populated by Fabricantes
	When I make a DELETE request for a Fabricante of ID 3
	Then I receive a 200 status code from Fabricante request
	When I GET the Fabricante data back form the DB
	Then I receive a 200 status code from Fabricante request
	And the Fabricante of name "Dummy3" is not found

Scenario: Test the Fabricante DELETE operation for a non-existent element
	Given an empty Fabricante table
	When I make a DELETE request for a Fabricante of ID 5
	Then I receive a 204 status code from Fabricante request
	Given a table populated by Fabricantes
	When I make a DELETE request for a Fabricante of ID 6
	Then I receive a 404 status code from Fabricante request