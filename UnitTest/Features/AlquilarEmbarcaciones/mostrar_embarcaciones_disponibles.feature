Feature: mostrar embarcaciones disponibles


Scenario: El cliente desea saber las embarcaciones disponibles para una fecha en concreto.
	Given El cliente selecciona una fecha en concreto. 
	When El cliente realiza la busqueda.   
	Then El sistema mostrará la embarcaciones que corresponden a fecha seleccionada y que se encuentrar disponibles parcial o totalmente. 

Scenario: Los resultados de busqueda varian según el filtro de disponibilidad. 
	Given El cliente selecciona una fecha en concreto. 
	When El filtro de disponibilidad es por día.
	Then El sistema enseñará solo las embarcaciones que poseen disponibles todas los horas de esa fecha. 


Scenario: El cliente desea ver las embarcaciones de una fecha erronea
	Given El cliente selecciona una fecha
	When desea seleccionar un dia anterior al dia actual
	Then El sistema no permitirá al usuario seleccionar una fecha al dia actual. 

Scenario: El cliente desea alquilar el barco en una hora erronea
	Given El cliente selecciona una fecha
	When desea seleccionar un hora anterior a la hora actual
	Then El sistema no permitirá al usuario seleccionar una hora a la hora actual. 