# Conekta (.NET) [![conekta MyGet Build Status](https://www.myget.org/BuildSource/Badge/conekta?identifier=59be30f5-59fe-45a7-be8c-e45181cfb587)](https://www.myget.org/)



## Resumen


Actualmente, esta librería usa el REST API de [Conekta](http://www.conekta.io), versión 0.3. Los métodos han sido agregados según han sido requeridos en mis proyectos - si necesitas algún método en particular, avisame en Twitter ([@bscheiman](http://www.twitter.com/bscheiman)) y veo si lo puedo agregar.


## Métodos

Todos los objetos complejos tienen una propiedad IsError. Si es true... ya tu sabe.

### ConektaLib(string key)
Constructor. Si no especificas una llave (la privada de Conekta, ¿EEEHHH?)

### Subscription CancelSubscription(string clientId)
Cancela la suscripción del cliente, cualquiera que esta sea.


### Charge Charge(string cardId, float amount, string currency = "MXN", string description = "")
Genera un cargo a la tarjeta *cardId* según el monto descrito en *amount* (1 = 1, no 0.01), la moneda descrita en *currency* (yay, ISO!) y la descripción especificada.

La tarjeta debe de ser tokenizada según el tutorial de formas de Conekta.


### ClientResponse CreateClient(string name, string email, string phone = null, string[] cards = null, string plan = null, string billingAddress = null, string shippingAddress = null, string rfc = null)
Crea un cliente en Conekta según los atributos especificados.

### Subscription CreatePlan(string plan, string name, int amount, string currency = "MXN", string interval = "month", int trial = 7, int frequency = 1, int expiry = 3)
Crea un plan/suscripción genérico. **NO** se le asigna a un cliente.


### bool DeleteClient(string clientId)
Borra un cliente. Nota, esto borra las tarjetas relacionadas.


### ClientResponse GetClient(string clientId)
Regresa toda la información relevante del cliente con id *clientId*


### Subscription PauseSubscription(string clientId)
Pausa la suscripción del cliente con id *clientId*


### Charge Refund(string chargeId)
Genera una devolución total del cargo especificado por *chargeId*


### Subscription ResumeSubscription(string clientId)
Reanuda la suscripción de *clientId*.


### bool SubscriptionExists(string plan)
Regresa `true` si el plan/suscripción ya existe... Y `false` si no


### Card SwitchCard(string clientId, string tokenId)
Cambia la tarjeta principal de *clientId* a la tarjeta especificada por *tokenId*


### bool TestCard(string cardId, float amount = 4, string currency = "MXN", string desc = "Cargo de prueba")
Genera un cargo de prueba para probar la tarjeta

## Actualizaciones

Junio 26, 2014: Documentación básica. Algunos métodos todavía aceptan null, pero eso cambiara pronto.

Junio 26, 2014++: Excepciones! Yay!
