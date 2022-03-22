Feature: get_activity_filtered

Scenario: Mostrar actividades activas con rangos de precios 
	Given se piden las actividades activas con un precio minimo de <minPrice> y maximo de <maxPrice>
	When se obtienen las actividades activas con ese rango de precio
	Then el resultado debe ser una lista con los barcos con un precio mayor a <minPrice> y menor a <maxPrice> que se encuentren activas

Examples:
| minPrice | maxPrice |
| 300      | 500      |
| 200      | 340      |  

Scenario: Mostrar actividades indicada con nombre introducido por el usuario
	Given se piden las actividades activas con nombre <activityName>
	When se obtienen las actividades activas con ese nombre
	Then el resultado debe ser una lista con todas las actividades activas con nombre <activityName>

Examples:
| activityName |
| Buceo |
| Pesca |			