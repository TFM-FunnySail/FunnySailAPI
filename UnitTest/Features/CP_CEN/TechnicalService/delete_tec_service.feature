Feature: delete_tec_service
	Simple calculator for adding two numbers

Scenario: Eliminar servicio técnico que no existe
	Given se quiere eliminar el servicio de id 50002
	When se intenta eliminar
	Then devuelve una excepcion de tipo not found

Scenario: Eliminar servicio técnico sin barcos
	Given se quiere eliminar el servicio de id 1001, que no tiene barcos
	When se intenta eliminar
	And se recupera el servicio actualizado para comprobar su estado
	Then el servicio técnico no existe

Scenario: Eliminar servicio técnico con barcos
	Given se quiere eliminar el servicio 1 con barcos utilizandolo
	When se intenta eliminar
	And se recupera el servicio actualizado para comprobar su estado
	Then el servicio técnico está desactivado