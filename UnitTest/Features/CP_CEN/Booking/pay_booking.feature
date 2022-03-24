Feature: PayBooking

Scenario: Se quiere marcar una reserva como pagada
	Given se pasa el identificador de una reserva
	When se invoca la función para pagar la reserva
	Then se marca la reserva como pagada

Scenario: Se quiere marcar una reserva como pagada con un identificador incorrectos
	Given se pasa un identificador inválido
	When se invoca la función para pagar la reserva
	Then no se marca la reserva como pagada