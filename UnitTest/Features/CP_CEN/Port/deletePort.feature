Feature: deletePort

Scenario: Se quiere eliminar un puerto
	Given un puerto con su identificador 1
	When se procede a la eliminación
	Then se elimina el puerto correctamente

Scenario: Se quiere eliminar un puerto que tiene reservas activas
	Given un puerto que aún tiene que ofrecer servicios a clientes 
	When se procede a la eliminación
	Then no se podrá proceder con la eliminación