Feature: create_refund

Scenario: Se quiere tramitar una devolución
    Given un grupo de datos para tramitar una devolución
    When se invoca la función de crear devolución
    Then se crea la devolución

Scenario: Se quiere tramitar una devolución incorrecta
    Given un grupo de datos para tramitar una devolución con datos incorrectos
    When se invoca la función de crear devolución
    Then no se crea la devolución