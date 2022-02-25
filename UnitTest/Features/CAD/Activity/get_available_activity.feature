Feature: get_available_activity


Scenario: Mostrar actividades activas con rangos de precios 
	Given se piden las actividades disponibles con un precio minimo de <minPrice> y maximo de <maxPrice>
	When se obtienen las actividades disponibles con ese rango de precio
	Then el resultado debe ser una lista con los barcos con un precio mayor a <minPrice> y menor a <maxPrice> que se encuentren activas

Examples:
| minPrice | maxPrice |
| 300      | 500      |
| 200      | 340      |  

Scenario: Mostrar actividades activas con rangos de fechas
	Given se piden las actividades disponibles para las fechas <initialDate> y <endDate>
	When se obtienen las actividades disponibles con esos rangos de fechas
	Then el resultado debe ser una lista con todas las actividades activas entre <initialDate> y <endDate>

Examples:
| initialDate | endDate  |
| 2022-02-02T11:00    | 2022-04-06T11:00 |
| 2022-02-21T14:00    | 2022-04-03T14:00 |

Scenario: Mostrar actividades indicada con nombre introducido por el usuario
	Given se piden las actividades disponibles con nombre <activityName>
	When se obtienen las actividades disponibles
	Then el resultado debe ser una lista con todas las actividades activas con nombre <activityName>

Examples:
| activityName |
| Buceo |
| Pesca |					