Feature: technical_service_persitence

Scenario: Adiciono un servicio técnico
	Given con precio y descripción <price>,<description>
	When se adiciona el servicio técnico
	Then devuelve el servicio técnico creado en base de datos con los mismos valores

Examples: 
| price   | description          |
| 10  | Reparación de motor |
| 20 | Pintado      |
| 25  | Servicio electrónico     |

Scenario: Adiciono un servicio técnico sin descripcion
	Given con precio <price>, sin descripcion
	When se adiciona el servicio técnico
	Then devuelve un error porque la descripcion del servicio técnico es requerida es requerida

Examples: 
| price   | description          |
| 10  | Reparación de motor |
| 20 | Pintado      |
| 25  | Servicio electrónico     |