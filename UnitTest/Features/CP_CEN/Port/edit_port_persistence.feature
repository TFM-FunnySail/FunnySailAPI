Feature: edit_port_persitence

Scenario: Edito un puerto
	Given se pretende editar el <nombre> y <location> del puerto con <id>
	When se edita el puerto
	Then devuelve el puerto editado en base de datos con los valores actualizados

Examples: 
| id | name  | location |
| 1  | Puerto de la amargura | c/Río Tajo  |

Scenario: Edito un puerto con nombre vacio
	Given se pretende editar un puerto con <id> y <name> dejando la localización vacia
	When  se edita el puerto
	Then devuelve un error porque la localizacion del puerto es requerida

Examples: 
| id | name                  |
| 1  | Puerto de la amargura |