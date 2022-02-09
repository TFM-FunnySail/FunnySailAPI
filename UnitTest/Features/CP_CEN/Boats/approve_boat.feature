Feature: approve_boat

Scenario: Actualizar barco que no existe
	Given que se quiere actualizar el barco de id 999999 y no existe
	When se aprueba el barco
	Then devuelve un error porque el barco no existe

Scenario: Aprobar barco
	Given que se quiere actualizar el barco de id 1
	When se aprueba el barco
	Then devuelve el barco aprobado