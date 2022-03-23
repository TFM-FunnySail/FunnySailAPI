Feature: RemoveImage

Scenario: Se quiere eliminar una imagen
	Given se pasan los identificadores correctos
	When se invoca la función para eliminar la imagen
	And se comprueban los datos eliminados
	Then se comprueba que los datos fueron eliminados

Scenario: Se quiere eliminar una imagen que no existe
	Given se pasan el id de la imagen -1
	When se invoca la función para eliminar la imagen
	Then lanza una excepcion NotFound

Scenario: Intento no permitido de eliminar imagen
	Given un usuario no propietario del barco ni admin quiere eliminar la imagen
	When se invoca la función para eliminar la imagen
	Then lanza una excepcion Forbidden