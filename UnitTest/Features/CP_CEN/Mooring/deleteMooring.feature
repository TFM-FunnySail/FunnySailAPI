Feature: deleteMooring

Scenario: Se quiere eliminar un punto de amarre sin barco asignado del sistema
	Given un punto de amarre que no tiene un barco asignado 1
	When se intente eliminar el amarre
	Then se eliminará el amarre con éxito

Scenario: Se quiere eliminar un punto de amarre con un barco asignado del sistema
	Given un punto de amarre que tiene un barco asignado 0
	When se intente eliminar el amarre
	Then el sistema devolverá error porque habrá un barco en ese amarre
