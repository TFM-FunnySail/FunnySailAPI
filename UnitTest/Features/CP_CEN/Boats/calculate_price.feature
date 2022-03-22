Feature: calculate_price
	Simple calculator for adding two numbers

Scenario: Calculo de precio
	Given un barco con precio por hora <priceByHour>, un precio por dia <priceByDay> y suplemento <suplement>
	And se quiere saber el precio para las horas <hours> y dias <days>
	When se calcula el precio del barco
	Then el resultado seria <result>

Examples: 
| priceByHour | priceByDay | suplement | hours | days | result |
| 10          | 8          | 5         | -1    | -1   | 0      |
| 10          | 8          | 5         | -3    | 20   | 165    |
| 10          | 8          | 5         | 10    | 0    | 105    |