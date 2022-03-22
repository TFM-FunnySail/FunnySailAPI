Feature: cancel_owner_ invoice

Scenario: Cancelar la factura del propietario
	Given que se quiere cancelar una factura de un propietario id 1
	When se cancela una factura 
	Then se cancela la factura

Scenario: Cancelar la factura que no existe
	Given que se quiere cancelar una factura de un propietario id 99999
	When se cancela una factura
	Then devuelve un error de que no se ha encontrado la factura 