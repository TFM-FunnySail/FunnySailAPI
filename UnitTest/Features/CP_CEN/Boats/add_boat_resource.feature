Feature: AddBoatResource

Scenario: Dada una embarcación a la que se quiere adjuntar un recurso
	Given un identificador de embarcación y otro de recurso
	When se introducen los datos
	Then se aceptan los datos introducidos

Scenario: Se quiere adjuntar un recurso pero no se tiene uno de los identificadores
	Given unos datos incompletos
	When se trata de realizar la relación
	Then no se produce la relación por falta de datos