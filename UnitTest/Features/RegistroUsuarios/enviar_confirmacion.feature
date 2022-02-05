Feature: enviar confimación.

Scenario: Usuario cliente añadido correctamente a la base de datos. 
	Given El usuario se ha registrado correctamente. 
	When Se ha añadido a la base de datos.  
	Then Se le envia un correo de confimación al usuario al correo dado por el usuario. 

