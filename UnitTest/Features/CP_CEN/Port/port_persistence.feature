Feature: port_persitence

Scenario: Adiciono un puerto
	Given con un <name> y una <location>
	When se adiciona el puerto
	Then devuelve el puerto creado en base de datos con los mismos valores

Examples: 
| name | location | 
| Puerto de la felicidad | c/Río Tajo  | 
| Puerto de la tristeza  | c/Río Ebro  | 

Scenario: Adiciono un puerto sin nombre
	Given con una <location> y sin nombre
	When se adiciona el puerto
	Then devuelve un error porque el nombre del puerto es requerido

Examples: 
| location | 
| c/Río Tajo  | 
| c/Río Ebro  | 

