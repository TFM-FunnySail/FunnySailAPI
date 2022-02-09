Feature: disapprove_boat

Scenario: Desaprobar barco que no existe
	Given que se quiere desaprobar el barco de id 999, el admin de id 1
	When se desaprueba el barco
	Then no desaprueba el barco y devuelve un error porque el barco no existe

Scenario: Desaprobar barco sin observación
	Given que se quiere desaprobar el barco de id 2, el admin de id 1, sin observación
	When se desaprueba el barco
	Then devuelve un error diciendo que la observación es requerida

Scenario: Admin desconocido desaprueba barco
	Given que se quiere desaprobar el barco de id 2, el admin de id 9999
	When se desaprueba el barco
	Then devuelve un error diciendo que el admin no existe

Scenario: Desaprobar barco
	Given que se quiere desaprobar el barco de id <id>, el admin de id <adminId> y la observación <observ>
	When se desaprueba el barco
	Then devuelve el barco desaprobado

Examples:
| id | adminId | observ            |
| 1  | 1       | estamos probando  |
| 2  | 1       | seguimos probando |