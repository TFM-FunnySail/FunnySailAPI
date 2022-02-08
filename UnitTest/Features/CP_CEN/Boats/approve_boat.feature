Feature: approve_boat
	Simple calculator for adding two numbers

Scenario: Actualizar barco que no existe
	Given que se quiere actualizar el barco de id 999999 y no existe
	When se adiciona el barco
	Then devuelve un error porque el barco no existe