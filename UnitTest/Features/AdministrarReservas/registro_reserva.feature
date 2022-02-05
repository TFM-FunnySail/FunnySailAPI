Feature: registro_reserva

Scenario: Crear una nueva reserva por el administrador
	Given se intenta crear una nueva reserva por el administrador
	When se insertan los siguientes datos como nueva reserva: <client>,<emb>,<horario>,<espc>,<fianza>,<total>
	Then se crea la nueva reserva con los datos: <client>,<emb>,<horario>,<espc>,<fianza>,<total>,<apagar>
	And se le notifica al amdinistrador que la reserva fue creada correctamente
	And se envia un correo al cliente informandole de la reserva ha sido creada correctamente e indicando el monto faltante <apagar>

| client           | emb                          | horario                 | espc        | fianza        | total 	| apagar	|
| royher@gmail.com | id: 23, desc:"barco grande" | 20 julio: 12:00 - 17:00 | con capitán | 100 | 220 | 120 |
| elsawong@gmail.com | id: 12, desc:"barco pequeño" | 21 julio: 11:00 - 13:00 | sin capitán | 50 | 150 | 100 |

Scenario: Registro de quien creo la reserva
	Given el administrador <admin> crea un nueva reserva
	When se crea una nueva reserva
	Then se crea una nueva reserva en base de datos
	And se registra que el usuario <admin> creó la reserva de id:<alq>

| admin          | alq |
| caty@gmail.com | 234 |
| orlando@gmail.com | 235 |
| andres@gmail.com | 236 |
| mohamed@gmail.com | 237 |

Scenario: Registro de reserva con una embarcación no disponible
	Given la embarcación seleccionada no tiene fechas disponibles
	When se selecciona una embarcación no disponible
	Then se notifica que la embarcación no está disponible para reservar en esa fecha
