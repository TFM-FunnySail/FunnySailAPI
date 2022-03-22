Feature: add_mooring

Scenario: anyadir un amarre 
	Given el amarre tiene como alias el Amarre 1 y id puerto el 1 y tipo small 
	When se anyade el amarre 
	Then devuelve el id de mooring 

Scenario: anyadir un amarre con un id incorrecto  
	Given el amarre tiene como id puerto el -1 
	When se anyade el amarre 
	Then devuelve el error id no es correcto

Scenario: anyadir un amarre con un alias vacio  
	Given  el amarre tiene como id 1 y tipo small
	When se anyade el amarre 
	Then devuelve el error alias es requerido