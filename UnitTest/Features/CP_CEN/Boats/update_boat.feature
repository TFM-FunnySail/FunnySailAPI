Feature: update_boat

Scenario:Actualizar embarcación sin el boatType
	Given los datos del actualización de la embarcación sin boatType
	When se actualizan los datos de la embarcación
	Then salta una excepción boat type dont exist

Scenario:Actualizar embarcación sin el MooringId 
	Given los datos del actualización de la embarcación sin MooringId
	When se actualizan los datos de la embarcación
	Then salta una excepción MooringId dont exist

Scenario:Actualizar embarcación sin el BoatID 
	Given los datos del actualización de la embarcación sin BoatID
	When se actualizan los datos de la embarcación
	Then salta una excepción BoatID dont exist


Scenario:Actualizar embarcación con los datos correctos 
	Given los datos del actualización de la embarcación
	When se actualizan los datos de la embarcación
	Then se actualizan los datos