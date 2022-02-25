Feature: edit_activity_persitence

Scenario: Edito una actividad
	Given se pretende editar el <nombre> de la actividad con <id>
	When se edita la actividad
	Then devuelve la actividad editada en base de datos con los valores actualizados

Examples: 
| id | name  |
| 1  | Wakeboard |
| 2  | Navegar  |

Scenario: Edito una actividad con nombre vacio
	Given se pretende editar la actividad con <id> dejando el nombre vacio
	When  se edita la actividad
	Then devuelve un error porque el nombre de la actividad es requerido

Examples: 
| id |
| 1  | 
| 2  | 
