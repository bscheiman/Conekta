#region
using System;
using Conekta.Objects;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

#endregion

namespace Conekta.Tests {
    [TestFixture]
    public class BugReportTests {
        [Test]
        public void TestBugReport1() {
            var json =
                @"{""id"":""XXXXXXXX"",""livemode"":false,""created_at"":1437156099,""status"":""paid"",""currency"":""MXN"",""description"":""Hola"",""reference_id"":null,""failure_code"":null,""failure_message"":null,""monthly_installments"":null,""object"":""charge"",""amount"":1000,""paid_at"":1437156100,""fee"":324,""customer_id"":"""",""refunds"":[],""payment_method"":{""name"":""XXX XXX"",""exp_month"":""12"",""exp_year"":""19"",""auth_code"":""000000"",""object"":""card_payment"",""type"":""credit"",""last4"":""4242"",""brand"":""visa"",""issuer"":"""",""account_type"":"""",""country"":""US"",""fraud_score"":29,""fraud_indicators"":[{""description"":""En las últimas 6 horas la persona ha intentado realizar compras con más de una tarjeta distinta; por seguridad se ha bloqueado la compra."",""weight"":29},{""description"":""El dispositivo con el que se hizo la compra se encuentra fuera de México y la persona tiene asociada a ella distintas tarjetas, correos, direcciones IP, etc. Este patrón es muy común en ataques; por seguridad se ha bloqueado la compra."",""weight"":null},{""description"":""La transacción se ha aprobado a través de un pago vía la modalidad Paga Móvil de Banorte."",""weight"":null}]},""details"":{""name"":null,""phone"":null,""email"":""XXXX@hotmail.com"",""line_items"":[{""name"":""Cosa"",""description"":""Cosa de cosas"",""unit_price"":10,""quantity"":1,""sku"":""1"",""category"":""Orden a la carta""}]}}";
            var obj = json.FromJson<Charge>();
            Console.WriteLine(JObject.FromObject(obj).ToString());
            Assert.IsNotNull(obj);

            Assert.AreEqual(obj.Id, "XXXXXXXX");
            Assert.AreEqual(obj.LiveMode, false);
            Assert.AreEqual(obj.CreatedAt, 1437156099);
            Assert.AreEqual(obj.Status, "paid");
            Assert.AreEqual(obj.Currency, "MXN");
            Assert.AreEqual(obj.Description, "Hola");
            Assert.AreEqual(obj.ReferenceId, null);
            Assert.AreEqual(obj.FailureCode, null);
            Assert.AreEqual(obj.FailureMessage, null);
            Assert.AreEqual(obj.MonthlyInstallments, null);
            Assert.AreEqual(obj.Object, "charge");
            Assert.AreEqual(obj.Amount, 1000);
            Assert.AreEqual(obj.PaidAt, 1437156100);
            Assert.AreEqual(obj.Fee, 324);
            Assert.AreEqual(obj.CustomerId, string.Empty);
            Assert.IsEmpty(obj.Refunds);
            Assert.IsNotNull(obj.PaymentMethod);
            Assert.AreEqual(obj.PaymentMethod.Name, "XXX XXX");
            Assert.AreEqual(obj.PaymentMethod.ExpMonth, 12);
            Assert.AreEqual(obj.PaymentMethod.ExpYear, 19);
            Assert.AreEqual(obj.PaymentMethod.AuthCode, "000000");
            Assert.AreEqual(obj.PaymentMethod.Object, "card_payment");
            Assert.AreEqual(obj.PaymentMethod.Type, "credit");
            Assert.AreEqual(obj.PaymentMethod.Last4, "4242");
            Assert.AreEqual(obj.PaymentMethod.Brand, "visa");
            Assert.AreEqual(obj.PaymentMethod.Issuer, string.Empty);
            Assert.AreEqual(obj.PaymentMethod.AccountType, string.Empty);
            Assert.AreEqual(obj.PaymentMethod.Country, "US");
            Assert.AreEqual(obj.PaymentMethod.FraudScore, 29);
            Assert.IsNotNull(obj.PaymentMethod.FraudIndicators);
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[0].Description, "En las últimas 6 horas la persona ha intentado realizar compras con más de una tarjeta distinta; por seguridad se ha bloqueado la compra.");
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[0].Weight, 29);
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[1].Description, "El dispositivo con el que se hizo la compra se encuentra fuera de México y la persona tiene asociada a ella distintas tarjetas, correos, direcciones IP, etc. Este patrón es muy común en ataques; por seguridad se ha bloqueado la compra.");
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[1].Weight, null);
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[2].Description, "La transacción se ha aprobado a través de un pago vía la modalidad Paga Móvil de Banorte.");
            Assert.AreEqual(obj.PaymentMethod.FraudIndicators[2].Weight, null);
            Assert.IsNotNull(obj.Details);
            Assert.AreEqual(obj.Details.Name, null);
            Assert.AreEqual(obj.Details.Phone, null);
            Assert.AreEqual(obj.Details.Email, "XXXX@hotmail.com");
            Assert.IsNotEmpty(obj.Details.LineItems);
            Assert.AreEqual(obj.Details.LineItems[0].Name, "Cosa");
            Assert.AreEqual(obj.Details.LineItems[0].Description, "Cosa de cosas");
            Assert.AreEqual(obj.Details.LineItems[0].UnitPrice, 10);
            Assert.AreEqual(obj.Details.LineItems[0].Quantity, 1);
            Assert.AreEqual(obj.Details.LineItems[0].Sku, "1");
            Assert.AreEqual(obj.Details.LineItems[0].Category, "Orden a la carta");

            /*{
        "name": "Cosa",
        "description": "Cosa de cosas",
        "unit_price": 10,
        "quantity": 1,
        "sku": "1",
        "category": "Orden a la carta"
      }
    ]
  }
}*/
        }
    }
}