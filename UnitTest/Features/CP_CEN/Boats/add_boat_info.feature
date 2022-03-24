Feature: add_boat_info

Scenario: Se introduce una embarcación sin nombre
	Given la información del barco está incompleta
	When se introduce la informacion de la embarcación
	Then devuelve error por falta de datos y no se crea la embarcación

Scenario: Con los datos apropiados, se quiere añadir la información del barco
	Given un grupo de datos válidos
	When se introduce la informacion de la embarcación
	Then el proceso se realiza correctamente y se añade la información