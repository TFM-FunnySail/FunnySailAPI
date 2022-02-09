Feature: get_available_boats

Scenario: Fechas que no tienen ordenes
	Given se piden los barcos disponibles para las fechas <initialDate> y <endDate>,no hay reserva con esa fecha
	When se obtienen los barcos disponibles
	Then el resultado debe ser una lista con todos los barcos activos

Examples:
| initialDate | endDate  |
| 2022-04-01T20:00    | 2022-04-02T10:00 |
| 2022-04-05T10:00    | 2022-04-07T12:00 |

Scenario: Fechas que tienen ordenes
	Given se piden los barcos disponibles para las fechas <initialDate> y <endDate> y los barcos 1 y 2 estan reservados para esas fechas
	When se obtienen los barcos disponibles
	Then el resultado debe ser una lista sin los barcos 1 y 2


Examples:
| initialDate | endDate  |
| 2022-04-01T20:00    | 2022-04-02T10:00 |
| 2022-04-05T10:00    | 2022-04-07T12:00 |

Scenario: Fechas que no tienen ordenes y se pide un solo barco
	Given se piden los barcos disponibles para las fechas <initialDate> y <endDate>,no hay reserva con esa fecha y se pide un solo barco
	When se obtienen los barcos disponibles
	Then el resultado debe ser una lista con solo un barco

Examples:
| initialDate | endDate  |
| 2022-04-01T20:00    | 2022-04-02T10:00 |
| 2022-04-05T10:00    | 2022-04-07T12:00 |