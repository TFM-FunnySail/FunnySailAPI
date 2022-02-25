Feature: add_boat_info

Scenario: Se introduce una embarcación sin nombre
	Given la información del barco está incompleta
	When se introducen los datos incompletos
	Then devuelve error por falta de datos y no se crea la embarcación

Scenario: Con los datos apropiados, se quiere añadir la información del barco
	Given un grupo de datos válidos
	When introducimos los datos
	Then el proceso se realiza correctamente y se añade la información