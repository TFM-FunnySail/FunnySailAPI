Feature: mostrar_precios_reserva

Scenario: Se revisa el precio de fianza de una reserva junto con el precio de alquiler
	Given La reserva tiene un precio de <precioReservaDB> EUR
  And Un precio de alquiler de <precioAlquilerDB> EUR
	When Se accede a la embarcación
	Then Se muestra la embarcación con el precio <precioReserva> EUR
  And con el precio <precioAlquiler> EUR

Scenario: Se revisa el precio de reserva con un precio sin decimales
	Given La reserva tiene un precio sin decimales
	When Se accede a la reserva
	Then Se muestra el precio de la reserva y de alquiler con dos lugares decimales (.00)

Scenario: Se revisa la reserva de una embarcación en un horario pico
	Given La embarcación tiene una tarifa de reserva de <precioReservaDB> * 3 y de <precioAlqulerDB> * 3 desde las 20:00 hasta las 22:00
	When Se revisa el precio de la embarcación desde las 18:00 hasta las 22:00
	Then Se muestra el precio <precioReservaHoraPico> 
  And Se muestra el precio <precioAlquilerHoraPico>

| precioReservaDB | precioReserva | precioReservarHoraPico | precioAlquilerDB | precioAlquiler | precioReservaHoraPico |
| 10              | 10.00             | 30.00              | 25    | 25.00 | 75.00 |
| 13.50           | 13.50             | 40.50              | 29.50 | 29.50 | 88.50 |
| 17.50           | 17.50             | 52.50              | 33    | 33.00 | 99.00 |
| 15              | 15.00             | 45.00              | 32.50 | 32.50 | 97.50 |
