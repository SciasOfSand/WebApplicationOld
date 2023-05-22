@prod
Feature: Produto

A short summary of the feature

Scenario: Test the Produto GET operation in an empty DB
	Given an empty Produto table
	When I make a Produto GET request
	Then I receive a 204 status code from Produto request

Scenario: Test the Produto GET operation in a populated DB
	Given a table populated by Produtos
	When I make a Produto GET request
	Then I receive a 200 status code with the following Produto List:
		| id | nome   | 
		| 1  | Dummy1 |
		| 2  | Dummy2 |
		| 3  | Dummy3 |

Scenario: Test the Produto POST operation
	Given a table populated by Produtos
	When I make a POST request for a Produto named "Dummy4" and of Fabricante id 1:
	Then I receive a 200 status code from Produto request
	When I GET the Produto data back form the DB
	Then I receive a 200 status code from Produto request
	And there contains a Produto named "Dummy4"

Scenario: Test the Produto POST operation with a null object
	Given a table populated by Produtos
	When I make a Produto POST request using a null JSON
	Then I receive a 204 status code from Produto request

Scenario: Test the Produto POST operation with incomplete info
	Given a table populated by Produtos
	When I make a Produto POST request using the following JSON:
	| id   | nome |
	|      |      |
	Then I receive a 400 status code from Produto request

Scenario: Test the Produto PUT operation
	Given a table populated by Produtos
	When I make a PUT request for a Produto of id 3, naming it "Dummy0":
	Then I receive a 200 status code from Produto request
	When I GET the Produto data back form the DB
	Then I receive a 200 status code from Produto request
	And the Produto of id 3 is now named "Dummy0"

Scenario: Test the Produto PUT operation with a null object
	Given a table populated by Produtos
	When I make a Produto PUT request using a null JSON
	Then I receive a 204 status code from Produto request

Scenario: Test the Produto PUT operation with incomplete info
	Given a table populated by Produtos
	When I make a Produto PUT request using the following JSON:
	| id   | nome |
	|      |      |
	Then I receive a 400 status code from Produto request

Scenario: Test the Produto PUT operation for a non-existent element
	Given a table populated by Produtos
	When I make a Produto PUT request using the following JSON:
	| id | nome   |
	| 5  | Dummy0 |
	Then I receive a 404 status code from Produto request

Scenario: Test the Produto DELETE operation
	Given a table populated by Produtos
	When I make a DELETE request for a Produto of ID 3
	Then I receive a 200 status code from Produto request
	When I GET the Produto data back form the DB
	Then I receive a 200 status code from Produto request
	Then the Produto of name "Dummy3" is not found

Scenario: Test the Produto DELETE operation for a non-existent element
	Given an empty Produto table
	When I make a DELETE request for a Produto of ID 5
	Then I receive a 204 status code from Produto request
	Given a table populated by Produtos
	When I make a DELETE request for a Produto of ID 6
	Then I receive a 404 status code from Produto request