Feature: create_boat

Scenario: Se crea una entidad barco con datos correctos
	Given se indican el tipo de barco y el id del amarre correctos
	When se aceptan los datos como correctos
	Then se crea la entidad barco correspondiente