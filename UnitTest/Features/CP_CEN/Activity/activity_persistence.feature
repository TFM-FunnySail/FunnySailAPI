Feature: activity_persitence

Scenario: Adiciono una actividad
	Given con <name>, <price>,<description> y <activityDate>
	When se adiciona la actividad
	Then devuelve la actividad creada en base de datos con los mismos valores

Examples: 
| name | price | description | activityDate |
| Kayak| 40  | Clases para aprender kayak | 2022-02-21T14:00    |
| Surf | 50  | Clases para aprender surf  | 2022-02-02T11:00    |

Scenario: Adiciono una actividad sin nombre
	Given con precio <price>, <description>, <activityDate> y sin nombre
	When se adiciona la actividad
	Then devuelve un error porque el nombre de la actividad es requerida

Examples: 
| price | description | activityDate |
| 40  | Clases para aprender kayak | 2022-02-21T14:00    |
| 50  | Clases para aprender surf  | 2022-02-02T11:00    |
