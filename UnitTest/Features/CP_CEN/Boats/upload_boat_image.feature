Feature: upload_boat_image

Scenario: Se quiere subir una imagen de tipo erronea
	Given se tiene una imagen de tipo jit
	When se invoca la función para publicar imagen
	Then da un error porque la extencion no es válida

Scenario: Se quiere subir una imagen a un barco que no existe
	Given se subira una imagen a un barco que no existe
	When se invoca la función para publicar imagen
	Then da un error de tipo NotFound

Scenario: Se sube la image correctamente
	Given se subira una imagen correcta a un barco existente
	When se invoca la función para publicar imagen
	Then la imagen ha sido agregada en base de datos
