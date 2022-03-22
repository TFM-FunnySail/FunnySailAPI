Feature: updated_technical_services

Scenario: Se desea modificar el servicio tecnico con datos incorrectos
	Given Dado un id que no exista
	When Se modifique el servicio tecnico
	Then Dara una excepcion y no se actualizara

Scenario: Se desea modificar el servicio tecnico con datos correctos
	Given Dados un id correcto y los datos correctos
	When Se modifique el servicio tecnico
	Then Se actualizarán los datos correctamente.