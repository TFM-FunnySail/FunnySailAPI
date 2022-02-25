Feature: AddBoatPrices

Scenario: La información para indicar los precios está incompleta
	Given un grupo de datos incompleto
	When se quiere indicar los precios asignados a una embarcación
	Then se produce un error por no ajustarse al modelo de datos