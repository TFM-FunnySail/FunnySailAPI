Feature: create_booking

Scenario: Se quiere reservar un barco
	Given se introducen los datos para una reserva
	When se invoca a la función para reservar el barco
	Then se reserva el barco y se crea la reserva correctamente

Scenario: Se quiere reservar un barco pero los datos de la reserva no están completos
	Given los datos para reserva incompletos
	When se invoca a la función para reservar el barco
	Then no se completa la reserva