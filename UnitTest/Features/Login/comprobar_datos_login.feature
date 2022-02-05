Feature: comprobar_datos_login

Scenario: Datos correctos
	Given el usuario escribe su usuario y contraseña correctamente
	When el usuario le da loguear
	Then se valida que los datos son correctos
	And se inicia proceso para crear sesión al usuario

Scenario: Contraseña incorrecta
	Given el usuario escribe mal su contraseña
	When el usuario le da loguear
	Then se valida que los datos son incorrectos
	And se notifica al usuario que los datos son incorrectos

Scenario: Email incorrecto
	Given el usuario escribe mal su email
	When el usuario le da loguear
	Then se valida que los datos son incorrectos
	And se notifica al usuario que los datos son incorrectos