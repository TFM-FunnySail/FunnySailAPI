Feature: mostrar_precios
	
# Mostrar precios (Sistema API)
Scenario: El cliente desea saber el precio de alquiler de una embarcación.
	Given La embarcacion seleccionada por el cliente tiene un <precio de alquiler>.
	When El cliente accede a la embarcación.  
	Then El sistema mostará la embarcación con el <precio de alquiler>.

# El precio del alquiler puede variar en funcion de las fechas. (Sistema API)
Scenario: El precio de alquiler variara en función de las fechas de alquiler. 
	Given En los meses de verano hay más gente interesada en alquilar embarcaciones. 
	And Hay más gente solicitando alquileres. 
	When Llega una nueva temporada (temporada alta o baja). 
	Then El sistema actualizará el precio de las embarcaciones.  

| Precio de alquiler |
| 500                |
| 20                 |
| 23                 |
