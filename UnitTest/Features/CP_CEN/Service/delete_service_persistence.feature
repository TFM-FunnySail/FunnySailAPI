Feature: delete_service_persistence

Scenario: Borrar un servicio sin ordenes
	Given se pretende borrar el servicio con id: <id> que no tiene ordenes
	When se intenta eliminar el servicio
	Then devuelve el objeto borrado vacio

Examples: 
| id |
| 1  |

Scenario: Borrar un servicio con ordenes
	Given se pretende borrar el servicio con id: <id> que tiene ordenes
	When  se intenta eliminar el servicio
	Then devuelve un objeto sin activar

Examples: 
| id | description  |
| 2  | Descripcion editada servicio 2 |
