# Conekta (.NET) [![bscheiman-oss MyGet Build Status](https://www.myget.org/BuildSource/Badge/bscheiman-oss?identifier=08c97b37-02d4-4f49-9ac1-1592f059b930)](https://www.myget.org/)


## Resumen


Actualmente, esta librería usa el REST API de [Conekta](http://www.conekta.io), versión 0.3. Los métodos han sido agregados según han sido requeridos en mis proyectos - si necesitas algún método en particular, avisame en Twitter ([@bscheiman](http://www.twitter.com/bscheiman)) y veo si lo puedo agregar.

## Instalación

Install-Package [Conekta](https://www.nuget.org/packages/Conekta)

(o baja el src)

## Uso

Necesitas contar con una cuenta en Conekta... y haber leido parte de la documentacion del REST API.

La mayoria de los objetos cuentan con un operador implicito para crear un objeto con el id. Es decir, los metodos que piden un Client, puedes ahorrarte el `new Client { Id = "blah" }` y solo usar `"blah"`:

````csharp
var lib = new ConektaLib("...");
await lib.ChargeAsync("cliente_12309", ...);
````

Para hacer un cargo, necesitas el token. **No puedes enviar los datos en crudo de la tarjeta.**

Repito:

**NO PUEDES ENVIAR LOS DATOS EN CRUDO. Si insistes en enviarlos en crudo, por favor lee algun documento de PCI DSS. No quieres esa responsabilidad.**

### Pero me da flojera crear una pagina HTML para eso...

ÑO.

### Pero estoy usando una plataforma movil

Si usas Xamarin, usa Conekta.Xamarin. Si no, usa las nativas que ofrece Conekta.

### Continuamos...

Si quieres generar un cargo recurrente, hay de dos sopas: usar la libreria, o crear el plan usando el sitio de admon. Lo mas importante es el planId. Lo tienes que asignar a cada cliente + configurar el status para que se les haga el cargo semanal/anual/etc.

## ASP.NET MVC 5 Hook

````csharp
public class ConektaController : Controller {
    public async Task<ActionResult> Hook([ModelBinder(typeof (JsonNetModelBinder<ConektaJson>))] ConektaJson model) {
        // ...
    }
}
````
