Feature: create_owner_invoice

Scenario: Se crea una nueva factura y los parámetros introducidos son correctos
	Given que se quiere crear una factura y los parámetros son correctos
	When se invoca a la función
	Then se crea la factura

Scenario: Se crea una nueva factura y los parámetros introducidos son incorrectos
	Given que se quiere crear una factura con los parámetros en un formato incorrecto
	When se invoca a la función
	Then no se crea la factura