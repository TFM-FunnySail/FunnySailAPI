Feature: boatType_persistence

Scenario: Adiciono un tipo de embarcacion
	Given con nombre y descripción <name>,<description>
	When se adiciona la embarcacion
	Then devuelve la embarcación creada en base de datos con los mismos valores

Scenario: Adiciono un tipo de embarcacion
	Given con nombre <name>, sin descripcion
	When se adiciona la embarcacion
	Then devuelve un error porque la descripcion es requerida

Examples: 
| name   | description          |
| Buque  | Descripcion de Buque |
| Lancha | Desc de lancha       |
| Kayac  | Descrip de Kayac     |
