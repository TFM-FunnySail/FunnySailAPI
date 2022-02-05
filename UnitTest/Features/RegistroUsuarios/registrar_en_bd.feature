Feature: registrar_en_bd

Scenario: Añadir un usuario cliente.
	Given se registra el usuario cliente.
	When rellena los datos correctamente.
	Then se añade el usuario a la base de datos.

Scenario: Añadir un usuario cliente.
	Given se registra el usuario cliente. 
	When rellena los datos incorrectamente.
	Then El sistema pide los datos otra vez y no se registra en la base de datos.

Scenario: Añadir el usuario administrador.
	Given se registra el usuario administrador.
	When el administrador incluye otro usuario administrador. 
	Then se añade el usuario a la base de datos.
