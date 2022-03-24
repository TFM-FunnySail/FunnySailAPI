Feature: cancel_booking

Scenario: Se quiere cancelar la reserva de un barco
	Given los datos de una reserva
	When se invoca a la función para cancelar la reserva
	Then se cancela la reserva

Scenario: Se quiere cancelar la reserva de un barco pero los datos de la reserva no están completos
	Given los datos de una reserva incompletos
	When se invoca a la función para cancelar la reserva
	Then no se modifica la reserva