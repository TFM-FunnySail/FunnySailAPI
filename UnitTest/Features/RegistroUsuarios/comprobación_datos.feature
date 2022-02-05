Feature: comprobación de datos. 

Scenario: el usuario se esta registrando y envia los datos para comprobarlos. 
    Given el usuario ha rellenado el formulario. 
    When envia para la correspondiente comprobación.
    Then El sistema comprueba todos los datos. 

Scenario: el usuario se esta registrando y envia los datos para comprobarlos. 
    Given el usuario ha rellenado el formulario. 
    When envia para la correspondiente comprobación.
    Then El sitema comprueba que los datos son erroneos. 
    And El sistema envia un mensaje de error con los datos que estan mal y pide los datos nuevamente.  


 