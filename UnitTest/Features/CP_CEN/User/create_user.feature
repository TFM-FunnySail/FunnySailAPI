Feature: create_user

Scenario: Se quiere crear un nuevo usuario y se han indicado los datos correspondientes de forma correcta
	Given un usuario con datos <email>,<nombre>,<apellidos>,<promocion>
	When se invoca la función de registro
	Then se crea un usuario con los mismos datos

Examples:
| email           | nombre  | apellidos | promocion |
| orla@gmail.com  | Orlando | Marques   | si        |
| andre@gmail.com | Andres  | Rojas     | no        |
| nino@yahoo.com  | Nino    | Ballester | si        |
| diego@gmail.com | Diego   | Juarez    | no        |
