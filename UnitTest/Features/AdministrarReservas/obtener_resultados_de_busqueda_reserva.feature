Feature: obtener_resultados_de_busqueda_reserva

Scenario: Buscar reservar con valor de fianza menor de un número
	Given en la base de datos hay <cantRes> reservas con costo menor de <maxFianza>
	When se filtra por las reservas con menor costo de <maxFianza>
	Then se muestran <cantRes> reservas

| cantRes | maxFianza |
| 7       | 30        |
| 15      | 50        |
| 30      | 100       |

Scenario: Buscar reservas pendientes de aprobar
	Given en la base de datos hay <cantRes> reservas pendientes de aprobar
	When se filtra por las reservas pendientes de aprobar
	Then se muestran <cantRes> reservas

	| cantRes |
	| 15      |
	
Scenario: Buscar numero de reservas de un cliente sin reservas
	Given el cliente <client> no tiene reservas
	When se buscan las reservas realizadas por el cliente <client>
	Then se muestra un alert indicando que no hay reservas que cumplan esas características

Scenario: Buscar numero de reservas de un cliente con reservas
	Given el cliente <client> tiene un historial de <cantRes> reservas realizadas
	When se piden todas las reservas realizadas por el cliente <client>
	Then se muestran <cantRes>

| client              | cantRes |
| pepe@gmail.com      | 2       |
| juan@gmail.com      | 3       |
| jose@yahoo.es       | 7       |

Scenario: Mostrar datos de una determinada reserva
	Given Se necesitan los datos de una reserva <idReserva>
	When Se introduce en el filtro de busqueda la <idReserva>
	Then Se muestran los datos de la reserva <client>,<precioTotal>,<idReserva>,<fechaReserva>,<emb>
  
| idReserva | client            | emb                          | fechaReserva | precioTotal |
| 100       | pepe@gmail.com    | id: 1, desc:"barco grande"   | 10/06/2021   | 100         |
| 101       | juan@gmail.com    | id: 2, desc:"barco medio"    | 15/06/2021   | 80          |
| 102       | antonio@gmail.com | id: 2, desc:"barco pequeño"  | 15/06/2021   | 50          |
