Feature: createUser

Scenario: Se quiere crear un nuevo usuario y se han indicado los datos correspondientes de forma correcta
	Given los datos de un nuevo usuario correctos
	When se registra el usuario
	Then se devuelve un mensaje de éxito en el registro

Scenario: El usuario que quiere registrarse no ha indicado correctamente su información
	Given unos datos incorrectos de registro de usuario
	When se intenta proceder con el registro
	Then no se permite continuar hasta solventar los fallos