#region
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Conekta.Objects;
using ServiceStack.Text;

#endregion

namespace Conekta {
    public class ConektaLib {
        private const string AppHeader = "application/vnd.conekta-v0.3.0+json";
        private const string BaseUrl = "https://api.conekta.io/";
        public string PublicKey { get; set; }

        public ConektaLib(string key) {
            JsConfig.IncludeNullValues = false;
            JsConfig.IncludePublicFields = true;

            PublicKey = key;
        }

        public Subscription CancelSubscription(string clientId) {
            return Post(string.Format("customers/{0}/subscription/cancel", clientId)).FromJson<Subscription>();
        }

        public Subscription ChangeSubscription(string clientId, string plan) {
            return Post(string.Format("customers/{0}/subscription", clientId), new {
                plan
            }).FromJson<Subscription>();
        }

        public Charge Charge(string cardId, float amount, string currency = "MXN", string description = "") {
            return Post("charges", new {
                description,
                amount = amount * 100,
                currency,
                card = cardId
            }).FromJson<Charge>();
        }

        public ClientResponse CreateClient(string name, string email, string phone = null, string[] cards = null, string plan = null,
            string billingAddress = null, string shippingAddress = null, string rfc = null) {
            return Post("customers", new {
                name,
                email,
                phone,
                cards,
                plan,
                billing_address = billingAddress,
                shipping_address = shippingAddress
            }).FromJson<ClientResponse>();
        }

        public Subscription CreatePlan(string plan, string name, int amount, string currency = "MXN", string interval = "month",
            int trial = 7, int frequency = 1, int expiry = 3) {
            if (SubscriptionExists(plan)) {
                return new Subscription {
                    Id = plan
                };
            }

            return Post("plans", new {
                id = plan,
                name,
                amount = amount * 100,
                currency,
                interval,
                frequency,
                trial_period_days = trial,
                expiry_count = expiry
            }).FromJson<Subscription>();
        }

        public bool DeleteClient(string clientId) {
            return Delete(string.Format("/customers/{0}", clientId)).FromJson<ClientResponse>().Deleted;
        }

        public ClientResponse GetClient(string clientId) {
            return Get(string.Format("customers/{0}", clientId)).FromJson<ClientResponse>();
        }

        public Subscription PauseSubscription(string clientId) {
            return Post(string.Format("customers/{0}/subscription/pause", clientId)).FromJson<Subscription>();
        }

        public Charge Refund(string chargeId) {
            return Post(string.Format("charges/{0}/refund", chargeId)).FromJson<Charge>();
        }

        public Subscription ResumeSubscription(string clientId) {
            return Post(string.Format("customers/{0}/subscription/resume", clientId)).FromJson<Subscription>();
        }

        public bool SubscriptionExists(string plan) {
            try {
                return !string.IsNullOrEmpty(Get(string.Format("plans/{0}", plan)).FromJson<Subscription>().Id);
            } catch {
                return false;
            }
        }

        public Card SwitchCard(string clientId, string tokenId) {
            var client = GetClient(clientId);

            foreach (var c in client.Cards)
                Delete(string.Format("customers/{0}/cards/{1}", clientId, c.Id));

            return Post(string.Format("customers/{0}/cards/", clientId), new {
                token = tokenId
            }).FromJson<Card>();
        }

        public bool TestCard(string cardId, float amount = 4, string currency = "MXN", string desc = "Cargo de prueba") {
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