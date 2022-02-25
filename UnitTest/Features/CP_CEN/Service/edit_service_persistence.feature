Feature: edit_service_persitence

Scenario: Edito un servicio
	Given se pretende editar la <descripcion> y <name> del servicio que tiene la id: <id>
	When se edita el servicio
	Then devuelve el servicio editado en base de datos con los valores actualizados

Examples: 
| id | description                    | name               |
| 1  | Descripcion editada servicio 1 | Servicio 1 editado |
| 2  | Descripcion editada servicio 2 | Servicio 2 editado |  

Scenario: Edito un servicio cambiando el nombre actual por uno vacio
	Given se pretende editar el servicio con <id> y <description> dejando el nombre vacio
	When  se edita el servicio
	Then devuelve un error porque el nombre del servicio es requerido

Examples: 
| id | description  |
| 1  | Descripcion editada servicio 1 |
| 2  | Descripcion editada servicio 2 |
