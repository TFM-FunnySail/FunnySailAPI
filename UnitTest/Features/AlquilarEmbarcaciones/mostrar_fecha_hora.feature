Feature: mostrar_fecha_hora
	
Scenario: El cliente desea saber la fecha disponibles de una embarcación
	Given La embarcacion seleccionada por el cliente tiene unas fechas disponibles. 
	When El cliente accede a la embarcación.  
	Then El sistema mostará la embarcación con el calendario de fechas disponibles. 

Scenario: El cliente desea alquilar un barco un solo día durante un par de horas.   
	Given El cliente desea saber si la embarcación esta disponible.
	When selecciona un día enconcreto y solo quiere alquilarlo un par de horas. 
	Then El sistema mostará la embarcación con el calendario de fechas disponibles, además enseñará un recuadro con las horas
    disponibles. 

Scenario: El cliente desea alquilar el barco en una fecha erronea
	Given El cliente selecciona una embarcacion.
	When desea seleccionar un dia anterior al dia actual
	Then El sistema no permitirá al usuario seleccionar una fecha al dia actual. 

Scenario: El cliente desea alquilar el barco en una hora erronea
	Given El cliente selecciona una embarcacion.
	When desea seleccionar un hora anterior a la hora actual
	Then El sistema no permitirá al usuario seleccionar una hora a la hora actual. 