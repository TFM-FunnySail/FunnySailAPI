Feature: AddRequireBoatTitle

Scenario: Se tiene un barco que supera los 6 metros
	Given un barco que necesita una titulación para manejarlo
	When el proceso de registro del barco esté en marcha
	Then se exigirá la especificación de un título válido para la embarcación

Scenario: una embarcación de más de 6 metros en la que no se especifica titulación
	Given un barco que requiere titulación por ley y no se especifica
	When se quiere continuar con el proceso de registro
	Then no se registrará el barco por falta de información