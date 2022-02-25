Feature: deactivate_activity


Scenario: Desactivar una actividad que se encontraba activada
	Given se pide desactivar una actividad con <id>
	When se desactiva la actividad seleccionada
	Then el resultado debe ser que la actividad con <id> se encuentra ahora desactivada

Examples:
| id |
| 1  | 
| 2  | 
| 3  | 

Scenario:  Desactivar una actividad que se encontraba desactivada
	Given se pide desactivar una actividad con <id>
	When se desactiva la actividad seleccionada
	Then el resultado debe ser que la actividad con <id> se encuentra ahora desactivada

Examples:
| id |
| 4  | 
| 5  | 				