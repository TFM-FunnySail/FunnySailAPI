Feature: crear_sesion

Scenario: Sesión de cliente
	Given se debe crear la sesión del cliente <client>
	When se validaron los datos del login del cliente
	Then se crea un token con los permisos del cliente <client>
	And se devuelve el token creado

| client              |
| ernn@gmail.com      |
| bclopez@gmail.com   |
| wenztifani@yahoo.es | 

Scenario: Sesión de administrador
	Given se intenta loguear un usuario administrador
	When se validaron los datos del usuario
	Then se crea un token con los permisos de administrador
	And se devuelve el token creado

Scenario: Sesión nueva
	Given se intenta loguear el usuario <user> con el role de <role> y los permisos de <claims>
	When se validaron los datos del usuario
	Then se crea un token con los permisos <claims> y el rol <role> para el usuario <user>
	And se devuelve el token creado

| user              | role      | claims                             |
| bclopez@gmail.com | cliente   |                                    |
| diego@gmail.com   | proveedor | subir_embarcaciones                |
| andres@gmail.com  | admin     | aprobar_pedido,aprobar_embarcacion |
