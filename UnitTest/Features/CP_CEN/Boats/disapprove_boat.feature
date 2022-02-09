Feature: disapprove_boat

Scenario: Desaprobar barco que no existe
	Given que se quiere desaprovar el barco de id 999999 y no existe
	When se desaprueba el barco
	Then devuelve un error porque el barco no existe

Scenario: Desaprobar barco sin observación
	Given que se quiere desaprobar el barco de id 2 sin observacíon
	When se desaprueba el barco
	Then devuelve un error diciendo que la observación es requerida

Scenario: Admin desconocido desaprueba barco
	Given un admin que no existe quiere desaprobar el barco de id 2
	When se desaprueba el barco
	Then devuelve un error diciendo que la observación es requerida