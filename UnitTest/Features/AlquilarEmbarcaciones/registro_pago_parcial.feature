Feature: registro_pago_parcial
	
Scenario: El cliente ha seleccionado una embarcación y desea realizar el pago parcial. 
	Given Seleccionada la embarcación y las horas.  
	When El cliente desea realizar el pago parcial.
	Then El sistema mostrá un formulario para poder introducir los datos de cobro. 

Scenario: El cliente ha introducido mal los datos del pago.   
	Given El cliente rellena el formulario. 
	When desea realizar el pago parcial del servicio. 
	Then El sistema mostará un mensaje de error y pedirá que el cliente introduzca los datos nuevamente. 


Scenario: El cliente selecciona el metodo de pago bizum. 
    Given El cliente rellena el formulario. 
    When Desea realizar el pago parcial del servicio mediante bizum. 
    Them El sistema espeara 5 minutos para el pago del cliente si el tiempo es superior a este no se registrará el pago.

Scenario: El cliente selecciona el metodo de pago transferencia bancaria. 
    Given El cliente rellena el formulario. 
    When Desea realizar el pago parcial del servicio mediante transferencia bancaria. 
    Them El sistema espeara 5 minutos para el pago del cliente si el tiempo es superior a este no se registrará el pago.
