Feature: modificar_reserva

Scenario: Se aprueba la reserva
	Given la reserva está pendiente de aprobar
	When se pone la reserva como aprobada
	Then se cambia el estado de la reserva por aprobada
	And se envia un correo al cliente informandole que su reserva fue aprobada
	And se informa en la página del admin que la reserva fue aprobada

Scenario: Cambio de horario de la reserva
	Given la embarcación de la reserva tiene los horarios que se quieren cambiar disponibles
	When se intenta cambiar el horario de reserva
	Then se cambia el horario de la reserva
	And se informa en la página del admin que la reserva fue modificada
	
Scenario: Cambio de horario de la reserva para un horario no disponible
	Given la embarcación no tiene horarios disponibles
	When se intenta cambiar el horario de la reserva
	Then no se cambia el horario de la reserva
	And se le notifica al administrador que el horario no fue modificado porque el seleccionado no está disponible
  
Scenario: Modificar cantidad pagada de la reserva
	Given se intenta cambiar la cantidad pagada de la reserva
	When la cantidad pagada no es la correcta
	Then se cambia el valor de la cantidad pagada
	And se le notifica al administrador que la cantidad pagada fue modificada correctamente
  And se le notifica al cliente el cambio realizado
