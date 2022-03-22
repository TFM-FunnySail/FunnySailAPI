Feature: updateMooring

Scenario: Se quieren modificar los datos de un amarre
	Given un objeto amarre con los datos necesarios
	When se proceda a actualizar el amarre
	Then se tomará el id del amarre del objeto y se sobreescribirán los datos que se introduzcan

Scenario: Se intenta modificar los datos de un amarre pero hay datos incorrectos
	Given un objeto amarre con datos incorrectos
	When se proceda a actualizar el amarre
	Then se devolverá un error con los datos del amarre