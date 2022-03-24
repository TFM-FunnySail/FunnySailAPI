Feature: createUser

Scenario: Se quiere crear un nuevo usuario y se han indicado los datos correspondientes de forma correcta
	Given los datos de un nuevo usuario correctos
	When se invoca la función de registro
	Then se notifica al usuario el éxito con un email

Scenario: El usuario que quiere registrarse no ha indicado correctamente su información
	Given unos datos incorrectos de registro de usuario
	When se invoca la función de registro
	Then no se permite continuar hasta solventar los fallos