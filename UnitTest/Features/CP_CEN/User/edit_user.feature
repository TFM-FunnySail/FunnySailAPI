Feature: EditUser

Scenario: Se quiere actualizar la información de un usuario
	Given un grupo de datos nuevos
	When se invoca la función para actualizar los datos del usuario
	Then se sobrescriben los datos nuevos sobre los viejos

Scenario: Se quieren actualizar los datos de un usuario pero no se pasa un objeto sino el nuevo dato a secas
	Given un grupo de datos nuevos e incorrectos
	When se invoca la función para actualizar los datos del usuario
	Then no se actualizarán los datos del usuario
