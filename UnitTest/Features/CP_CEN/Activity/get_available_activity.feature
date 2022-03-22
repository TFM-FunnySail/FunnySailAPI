Feature: get_available_activity

Scenario: Mostrar actividades activas con rangos de fechas
	Given se piden las actividades disponibles para las fechas <initialDate> y <endDate>
	When se obtienen las actividades disponibles con esos rangos de fechas
	Then el resultado debe ser una lista con todas las actividades activas entre <initialDate> y <endDate>

Examples:
| initialDate | endDate  |
| 2022-02-02T11:00    | 2022-04-06T11:00 |
| 2022-02-21T14:00    | 2022-04-03T14:00 |		