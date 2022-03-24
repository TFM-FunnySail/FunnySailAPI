Feature: login_user

Scenario: Login con datos incorrectos
	Given el usuario escribe una contraseña incorrecta
	When se intenta loguear el usuario
	Then debe devolver una excepción

Scenario: Login con datos correctos
	Given el usuario escribe los datos correctamente
	When se intenta loguear el usuario
	Then debe loguearse correctamente