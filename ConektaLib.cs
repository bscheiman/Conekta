#region
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using bscheiman.Common.Extensions;
using Conekta.Helpers;
using Conekta.Objects;
using ServiceStack.Text;

#endregion

namespace Conekta {
    /// <summary>
    /// Wrapper .NET para http://www.conekta.io
    /// </summary>
    public class ConektaLib {
        private const string AppHeader = "application/vnd.conekta-v0.3.0+json";
        private const string BaseUrl = "https://api.conekta.io/";
        internal string PublicKey { get; set; }

        /// <summary>
        /// Constructor. Necesitas una llave API (privada) para que todo funcione bien.
        /// </summary>
        /// <param name="key">Llave API</param>
        public ConektaLib(string key) {
            if (string.IsNullOrEmpty(key))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            JsConfig.IncludeNullValues = false;
            JsConfig.IncludePublicFields = true;

            PublicKey = key;
        }

        /// <summary>
        /// Cancela una suscripción.
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <returns>Un objeto representando la suscripción</returns>
        public Subscription CancelSubscription(string clientId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            return Post(string.Format("customers/{0}/subscription/cancel", clientId)).FromJson<Subscription>();
        }

        /// <summary>
        /// Cambia la suscripción de clientId para que use plan
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <param name="planId">Id del plan</param>
        /// <returns>Un objeto representando la suscripción</returns>
        public Subscription ChangeSubscription(string clientId, string planId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("planId");

            return Post(string.Format("customers/{0}/subscription", clientId), new {
                plan = planId
            }).FromJson<Subscription>();
        }

        /// <summary>
        /// Genera un cargo a una tarjeta de crédito o debito.
        /// </summary>
        /// <param name="cardId">Id de la tarjeta. Generalmente inicia con card_</param>
        /// <param name="amount">Monto en moneda entera, no centavos (1 = $1, no $0.01)</param>
        /// <param name="currency">Moneda a usar segun ISO 4217</param>
        /// <param name="description">Descripción del cargo</param>
        /// <returns>Un objeto representando el cargo</returns>
        public Charge Charge(string cardId, float amount, string currency = "MXN", string description = "Cargo Conekta") {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(cardId))
                throw new ArgumentNullException("cardId");

            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");

            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            if (amount <= 0)
                throw new ArgumentNullException("amount", "Parameter cannot be <= 0");

            return Post("charges", new {
                description,
                amount = amount * 100,
                currency,
                card = cardId
            }).FromJson<Charge>();
        }

        /// <summary>
        /// Crea un cliente en el backend de Conekta
        /// </summary>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="email">Correo</param>
        /// <param name="phone">Teléfono</param>
        /// <param name="cards">Arreglo de tarjetas</param>
        /// <param name="planId">Id de suscripción a usar</param>
        /// <param name="billingAddress">Dirección de la tarjeta</param>
        /// <param name="shippingAddress">Dirección para envío</param>
        /// <param name="rfc">RFC del cliente</param>
        /// <returns>Objeto representando al cliente</returns>
        public ClientResponse CreateClient(string name, string email, string phone = null, string[] cards = null, string planId = null,
            string billingAddress = null, string shippingAddress = null, string rfc = null) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            return Post("customers", new {
                name,
                email,
                phone,
                cards,
                plan = planId,
                billing_address = billingAddress,
                shipping_address = shippingAddress
            }).FromJson<ClientResponse>();
        }

        /// <summary>
        /// Crea una suscripción para que pueda ser usada con ChangeSub/CreateClient
        /// </summary>
        /// <param name="planId">Id a usar</param>
        /// <param name="name">Nombre</param>
        /// <param name="amount">Monto</param>
        /// <param name="currency">Moneda segun ISO 4217</param>
        /// <param name="interval">Intervalo a usar</param>
        /// <param name="trial">Días de prueba</param>
        /// <param name="frequency">Frecuencia del cargo</param>
        /// <param name="expiry">Número de intentos antes de cancelar la suscripción</param>
        /// <returns>Un objeto representando la suscripción</returns>
        public Subscription CreatePlan(string planId, string name, int amount, string currency = "MXN", Interval interval = Interval.Month,
            int trial = 7, int frequency = 1, int expiry = 3) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(planId))
                throw new ArgumentNullException("planId");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");

            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount", "Parameter cannot be negative.");

            if (SubscriptionExists(planId)) {
                return new Subscription {
                    Id = planId
                };
            }

            return Post("plans", new {
                id = planId,
                name,
                amount = amount * 100,
                currency,
                interval = interval.GetAttributeOfType<DescriptionAttribute>().Description,
                frequency,
                trial_period_days = trial,
                expiry_count = expiry
            }).FromJson<Subscription>();
        }

        /// <summary>
        /// Borra un cliente del backend de Conekta
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <returns>True/false</returns>
        public bool DeleteClient(string clientId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            return Delete(string.Format("/customers/{0}", clientId)).FromJson<ClientResponse>().Deleted;
        }

        /// <summary>
        /// Regresa los cargos disponibles en el backend
        /// </summary>
        /// <param name="tree">Árbol de búsqueda estilo LINQ (h => h.Amount == 8000)</param>
        /// <param name="limit">Límite por llamada</param>
        /// <param name="offset"># de página</param>
        /// <returns></returns>
        public Charge[] GetCharges(Expression<Func<Charge, bool>> tree = null, int limit = -1, int offset = -1) {
            var sb = new StringBuilder();

            if (tree != null)
                ExpressionHelper.GetQuery(tree, ref sb);

            if (limit >= 0)
                sb.AppendFormat("limit={0}&", limit);

            if (offset >= 0)
                sb.AppendFormat("offset={0}&", offset);

            var query = sb.ToString().TrimEnd('&');

            return Get(string.Format("charges?{0}", query)).FromJson<Charge[]>();
        }

        /// <summary>
        /// Regresa un cliente del backend de Conekta
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <returns>Un objeto representando al cliente</returns>
        public ClientResponse GetClient(string clientId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            return Get(string.Format("customers/{0}", clientId)).FromJson<ClientResponse>();
        }

        /// <summary>
        /// Pausa una suscripción
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <returns>Un objeto representando la suscripción</returns>
        public Subscription PauseSubscription(string clientId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            return Post(string.Format("customers/{0}/subscription/pause", clientId)).FromJson<Subscription>();
        }

        /// <summary>
        /// Genera una devolución TOTAL del cargo
        /// </summary>
        /// <param name="chargeId">Cargo a devolver</param>
        /// <returns>Un objeto representando el cargo</returns>
        public Charge Refund(string chargeId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(chargeId))
                throw new ArgumentNullException("chargeId");

            return Post(string.Format("charges/{0}/refund", chargeId)).FromJson<Charge>();
        }

        /// <summary>
        /// Reanuda la suscripción del cliente
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <returns>Un objeto representando la suscripción</returns>
        public Subscription ResumeSubscription(string clientId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            return Post(string.Format("customers/{0}/subscription/resume", clientId)).FromJson<Subscription>();
        }

        /// <summary>
        /// Checa si un plan existe o no
        /// </summary>
        /// <param name="planId">Id de la suscripción a checar</param>
        /// <returns>True/false</returns>
        public bool SubscriptionExists(string planId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(planId))
                throw new ArgumentNullException("planId");

            try {
                return !string.IsNullOrEmpty(Get(string.Format("plans/{0}", planId)).FromJson<Subscription>().Id);
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Configura el cliente para que use la tarjeta especificada.
        /// </summary>
        /// <param name="clientId">Id del cliente. Generalmente inicia con cus_</param>
        /// <param name="cardId">Id de la tarjeta a usar. Generalmente inicia con card_</param>
        /// <returns>Un objeto representando la tarjeta.</returns>
        public Card SwitchCard(string clientId, string cardId) {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");

            if (string.IsNullOrEmpty(cardId))
                throw new ArgumentNullException("cardId");

            var client = GetClient(clientId);

            foreach (var c in client.Cards)
                Delete(string.Format("customers/{0}/cards/{1}", clientId, c.Id));

            return Post(string.Format("customers/{0}/cards/", clientId), new {
                token = cardId
            }).FromJson<Card>();
        }

        /// <summary>
        /// Genera un cargo de prueba, luego lo devuelve.
        /// </summary>
        /// <param name="cardId">Id de la tarjeta. Generalmente inicia con card_</param>
        /// <param name="amount">Monto a usar. El mínimo es $4 MXN</param>
        /// <param name="currency">Moneda a usar</param>
        /// <param name="desc">Descripción del cargo</param>
        /// <returns>True/false</returns>
        public bool TestCard(string cardId, float amount = 4, string currency = "MXN", string desc = "Cargo de prueba") {
            if (string.IsNullOrEmpty(PublicKey))
                throw new InvalidKeyException("PublicKey hasn't been set.");

            if (string.IsNullOrEmpty(cardId))
                throw new ArgumentNullException("cardId");

            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");

            if (string.IsNullOrEmpty(desc))
                throw new ArgumentNullException("desc");

            if (amount <= 4)
                throw new ArgumentOutOfRangeException("amount", "Parameter cannot be <= 4.");

            var charge = Charge(cardId, amount, currency, desc);

            if (charge.IsError)
                return false;

            var refund = Refund(charge.Id);

            if (refund.IsError)
                return false;

            return refund.Status == "refunded";
        }

        #region Helpers
        internal string Delete(string endpoint) {
            return GetClient().DeleteAsync(GetEndpoint(endpoint)).Result.Content.ReadAsStringAsync().Result;
        }

        internal string Get(string endpoint) {
            return GetClient().GetStringAsync(GetEndpoint(endpoint)).Result;
        }

        internal HttpClient GetClient() {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:", PublicKey))));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AppHeader));

            return client;
        }

        internal Uri GetEndpoint(string endpoint) {
            return new Uri(string.Format("{0}{1}", BaseUrl, endpoint));
        }

        internal string Post(string endpoint, object obj = null) {
            obj = obj ?? new object();

            return
                GetClient()
                    .PostAsync(GetEndpoint(endpoint), new StringContent(obj.ToJson(), Encoding.UTF8, "application/json"))
                    .Result.Content.ReadAsStringAsync()
                    .Result;
        }
        #endregion
    }
}