Feature: edit_user


Scenario: Se quiere actualizar la información de un usuario
	Given dado el usuario <id>, se quiere editar con los valores siguientes <nombre>,<apellidos>,<promocion>,<birthday>
	When se invoca la función para actualizar los datos del usuario
	Then se sobrescriben los datos nuevos sobre los viejos

Examples:
| nombre        | apellidos      | promocion | birthday   |
| Pedro Edit    | Merten Editado | no        | 2000-01-01 |
| Rodri Editado | Quiez Editado  | si        | 1980-02-23 |