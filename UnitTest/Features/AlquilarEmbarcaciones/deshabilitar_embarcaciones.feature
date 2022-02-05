Feature: deshabilitar embarcaciones 

Scenario: El cliente ha reservado una embarcación todo el día. 
	Given El cliente ha realizado el pago de la embarcacion.
	When El sistema recibe la confimación del pago   
	Then El sistema cambiará el estado del barco a "reservado" y asignará el barco en esa fecha a ese cliente. 

Scenario:  El cliente ha reservado una embarcación durante unas horas. 
	Given El cliente ha realizado el pago de la embarcacion.
	When El sistema recibe la confimación del pago   
	Then El sistema cambiará el estado del barco a "reservado-parcial" y asignará el barco en esa fecha y a esas hora en concreto. 

Scenario: El dueño del barco desea reparar el barco y necesita deshabilitar el barco. 
    Given La embarcación necesita reparaciones
	When Se informa por parte del propietario a la empresa  
	Then El administrador cambiará el estado de barco a "no disponible"