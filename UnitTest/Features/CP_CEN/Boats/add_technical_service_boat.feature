Feature: add_technical_service_boat

Scenario: Se programa un servicio técnico para un barco
	Given una programación de un servicio técnico para un barco
	When se invoca a la función para añadirlo a la agenda
	Then se agenda la programación y se devuelve el id

Scenario: Se programa un servicio técnico para un barco pero no está disponible
	Given una programación de un servicio técnico para un barco no disponible
	When se invoca a la función para añadirlo a la agenda
	Then se devuelve un error de servicio ocupado

Scenario: Se programa un servicio técnico para un barco pero faltan datos
	Given una programación de un servicio técnico para un barco con datos incompletos
	When se invoca a la función para añadirlo la agenda
	Then se devuelve un error de que no se encuentra el servicio que se quiere programar