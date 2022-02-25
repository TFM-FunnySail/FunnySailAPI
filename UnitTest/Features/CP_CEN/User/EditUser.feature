Feature: EditUser

Scenario: Se quiere actualizar la información de un usuario
	Given dos objetos usuarios correctos
	When se quiera actualizar los datos
	Then se sustituirán los datos antiguos por los nuevos

Scenario: Se quieren actualizar los datos de un usuario pero no se pasa un objeto sino el nuevo dato a secas
	Given los datos del usuario pasados de forma incorrecta
	When se proceda a la actualización de los datos
	Then se devolverá un error en los datos introducidos para el usuario
