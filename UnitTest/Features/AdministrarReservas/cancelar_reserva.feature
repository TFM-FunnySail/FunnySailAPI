Feature: cancelar_reserva

Scenario: Cancelar una reserva pendiente de aprobar
	Given se intenta cancelar una reserva
	When la reserva está pendiente de aprobar 
	Then se cancela la reserva
	And se muestra un aviso en la página informando que la cancelación se realizó con éxito
	And se le envia un correo al cliente informandole de la cancelación

Scenario: Cancelar una reserva con la fianza pagada
	Given se intenta cancelar una reserva
	When la fianza de la reserva ya ha sido pagada
	Then se cancela la reserva
	And se crea un proceso nuevo para que sea devuelto la fianza de la reserva al cliente
	And se muestra un aviso en la página informando que la cancelación se realizó con éxito
	And se le envia un correo al cliente informandole de la cancelación y de la devolución del dinero de la fianza

Scenario: Cancelar una reserva ya consumida
	Given se intenta cancelar una reserva
	When la reserva ya ha sido consumida por el cliente
	Then no se cancela la reserva
	And se le informa al administrador que la reserva no se puede cancelar porque ya ha sido consumida por el cliente

Scenario: Completar el pago total del alquiler
	Given se intenta completar el pago <total> del alquiler menos la <fianza> aportada previamente
	When el pago <total> aun no ha sido pagado
	Then se paga el monto faltante <apagar>
	And se le notica al administrador que el pago a sido completado correctamente
  And se envia un correo al cliente informandole de la reserva ha sido pagada completamente y mostrandole la factura

|fianza | total | apagar |
| 100 | 220 | 120 |
| 50 | 150 | 100 |
