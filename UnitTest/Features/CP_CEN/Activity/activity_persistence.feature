Feature: activity_persitence

Scenario: Adiciono una actividad
	Given con <name>, <price>,<description>
	When se adiciona la actividad
	Then devuelve la actividad creada en base de datos con los mismos valores

Examples: 
| name | price | description | 
| Kayak| 40  | Clases para aprender kayak | 
| Surf | 50  | Clases para aprender surf  | 

Scenario: Adiciono una actividad sin nombre
	Given con precio <price>, <description> y sin nombre
	When se adiciona la actividad
	Then devuelve un error porque el nombre de la actividad es requerida

Examples: 
| price | description | 
| 40  | Clases para aprender kayak | 
| 50  | Clases para aprender surf  | 
