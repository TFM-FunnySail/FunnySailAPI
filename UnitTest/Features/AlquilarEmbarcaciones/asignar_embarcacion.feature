Feature: Asignar embarcación.

Scenario: El cliente ha reservado una embarcación todo el día. 
	Given El cliente ha realizado el pago de la embarcacion.
	When El sistema recibe la confimación del pago   
	Then El sistema asignará el barco en esa fecha a ese cliente. 

Scenario:  El cliente ha reservado una embarcación durante unas horas. 
	Given El cliente ha realizado el pago de la embarcacion.
	When El sistema recibe la confimación del pago   
	Then El sistema asignará el barco en esa fecha y a esas hora en concreto. 

