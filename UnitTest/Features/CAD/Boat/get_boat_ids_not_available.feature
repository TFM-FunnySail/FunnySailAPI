Feature: get_boat_ids_not_available

Scenario: Fechas que no tienen ordenes
	Given se piden los barcos disponibles para las fechas <initialDate> y <endDate> y no hay reserva con esa fecha
	When se obtienen los barcos no disponibles
	Then el resultado debe ser una lista vacía

Examples:
| initialDate | endDate  |
| 2022-04-01T20:00    | 2022-04-02T10:00 |
| 2022-04-05T10:00    | 2022-04-07T12:00 |

Scenario: Fechas que tienen ordenes
	Given se piden los barcos disponibles para las fechas <initialDate> y <endDate> y los barcos 1 y 2 estan reservados para esas fechas
	When se obtienen los barcos no disponibles
	Then el resultado debe ser una lista con los barcos 1 y 2


Examples:
| initialDate | endDate  |
| 2022-04-01T20:00    | 2022-04-02T10:00 |
| 2022-04-05T10:00    | 2022-04-07T12:00 |