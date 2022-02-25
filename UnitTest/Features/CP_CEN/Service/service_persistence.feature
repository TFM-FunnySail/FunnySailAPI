Feature: service_persistence

Scenario: Adiciono un servicio
	Given con los siguientes datos <description>, <price> y <name>
	When se adiciono el servicio
	Then devuelve el servicio creado en base de datos con los mismos valores

Examples: 
| name | price | description |
| Servicio 1 | 40  | Descripcion servicio 1 | 
| Servicio 2 | 50  | Descripcion servicio 2  |

Scenario: Adiciono un servicio sin descripcion
	Given con un <name>, un <price>
	When se adiciono el servicio sin descripcion
	Then devuelve un error porque la descripcion del servicio es requerida

Examples: 
| price | name |
| 40  | Servicio 3 | 
| 50  | Servicio 4 | 
