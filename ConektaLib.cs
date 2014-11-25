#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using bscheiman.Common.Extensions;
using Conekta.Objects;
using RestSharp;

#endregion

namespace Conekta {
    public class ConektaLib {
        private const string AppHeader = "application/vnd.conekta-v0.3.0+json";
        private const string BaseUrl = "https://api.conekta.io/";
        internal string PrivateKey { get; set; }

        public ConektaLib(string key) {
            if (string.IsNullOrEmpty(key))
                throw new InvalidKeyException("PrivateKey hasn't been set.");

            PrivateKey = key;
        }

        public Task<Card> AddCardAsync(Client client, string tokenId) {
            return PostAsync<Card>("customers/{clientId}/cards/", new {
                token = tokenId
            }, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Charge> ChargeAsync(Card card, float amount, string currency, string desc) {
            return PostAsync<Charge>("charges", new {
                description = desc,
                amount = (amount * 100),
                currency,
                card = card.Id
            });
        }

        public Task<Client> CreateClientAsync(string name, string email, string phone = null, string[] cards = null, string planId = null,
            string billingAddress = null, string shippingAddress = null, string rfc = null) {
            return PostAsync<Client>("customers", new {
                name,
                email,
                phone,
                cards,
                plan = planId,
                billing_address = billingAddress,
                shipping_address = shippingAddress
            });
        }

        public async Task<Subscription> CreateSubscriptionAsync(string planId, string name, int amount, string currency = "MXN",
            Interval interval = Interval.Month, int trial = 7, int frequency = 1, int expiry = 0) {
            if (await SubscriptionExists(planId)) {
                return new Subscription {
                    Id = planId
                };
            }

            return await PostAsync<Subscription>("plans", new {
                id = planId,
                name,
                amount = amount * 100,
                currency,
                interval = interval.GetAttributeOfType<DescriptionAttribute>().Description,
                frequency,
                trial_period_days = trial,
                expiry_count = expiry
            });
        }

        public Task<Card> DeleteCardAsync(Client client, Card card) {
            return DeleteAsync<Card>("customers/{clientId}/cards/{cardId}", new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            }, new Parameter {
                Name = "cardId",
                Value = card.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Client> DeleteClientAsync(Client client) {
            return DeleteAsync<Client>("customers/{clientId}", new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<List<Card>> GetAllCardsAsync(Client client) {
            return GetAsync<List<Card>>("customers/{clientId}/cards", null, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<List<Charge>> GetAllChargesAsync() {
            return GetAsync<List<Charge>>("charges");
        }

        public Task<List<Client>> GetAllClientsAsync() {
            return GetAsync<List<Client>>("customers");
        }

        public Task<Client> GetClientAsync(string clientId) {
            return GetAsync<Client>("customers/{customerId}", null, new Parameter {
                Name = "customerId",
                Value = clientId,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Subscription> GetSubscriptionAsync(string planId) {
            return GetAsync<Subscription>("plans/{planId}", null, new Parameter {
                Name = "planId",
                Value = planId,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Subscription[]> GetSubscriptionsAsync() {
            return GetAsync<Subscription[]>("plans");
        }

        public Task<Refund> RefundAsync(Charge charge) {
            return PostAsync<Refund>("charges/{chargeId}/refund", null, new Parameter {
                Name = "chargeId",
                Value = charge.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Subscription> SetSubscriptionForClientAsync(Client client, string planId) {
            return PostAsync<Subscription>("customers/{clientId}/subscription", new {
                plan = planId
            }, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Subscription> SetSubscriptionStatusForClientAsync(Client client, SubscriptionStatus status, string planId = "") {
            var statusParameter = new Parameter {
                Name = "status",
                Value = null,
                Type = ParameterType.UrlSegment
            };

            switch (status) {
                case SubscriptionStatus.Active:
                    statusParameter.Value = "resume";
                    break;

                case SubscriptionStatus.Paused:
                    statusParameter.Value = "pause";
                    break;

                case SubscriptionStatus.Canceled:
                    statusParameter.Value = "cancel";
                    break;
            }

            if (statusParameter.Value == null)
                throw new Exception("Invalid status");

            if (!string.IsNullOrEmpty(planId)) {
                PutAsync<Subscription>("customers/{clientId}", new {
                    plan = planId
                }, new Parameter {
                    Name = "clientId",
                    Value = client.Id,
                    Type = ParameterType.UrlSegment
                });
            }

            return PostAsync<Subscription>("customers/{clientId}/subscription/{status}", null, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            }, statusParameter);
        }

        private async Task<bool> SubscriptionExists(string planId) {
            try {
                return !string.IsNullOrEmpty((await GetAsync<Subscription>("plans/{planId}", null, new Parameter {
                    Name = "planId",
                    Value = planId,
                    Type = ParameterType.UrlSegment
                })).Id);
            } catch {
                return false;
            }
        }

        public async Task<Card> SwitchCardAsync(Client client, string cardToken) {
            if (client.Cards == null)
                client = await GetClientAsync(client.Id);

            foreach (var card in client.Cards)
                DeleteCardAsync(client, card);

            return await AddCardAsync(client, cardToken);
        }

        #region Helpers
        internal Task<T> DeleteAsync<T>(string url, params Parameter[] parameters) where T : new() {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.DELETE, null, parameters), response => tcs.SetResult(response.Content.FromJson<T>()));

            return tcs.Task;
        }

        internal Task<T> GetAsync<T>(string url, object obj = null, params Parameter[] parameters) {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.GET, obj, parameters), response => tcs.SetResult(response.Content.FromJson<T>()));

            return tcs.Task;
        }

        internal RestClient GetClient(string url) {
            var client = new RestClient(BaseUrl) {
                Authenticator = new HttpBasicAuthenticator(PrivateKey, ""),
                UserAgent = "Conekta.NET // @bscheiman"
            };

            return client;
        }

        internal RestRequest GetRequest(string url, Method method, object obj, params Parameter[] parameters) {
            var request = new RestRequest(url, method);

            foreach (var p in parameters)
                request.AddParameter(p);

            request.AddHeader("Accept", AppHeader);

            request.AddParameter("application/json", (obj ?? new {
            }).ToJson(), ParameterType.RequestBody);

            return request;
        }

        internal Task<T> PostAsync<T>(string url, object obj, params Parameter[] parameters) where T : new() {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.POST, obj, parameters), response => tcs.SetResult(response.Content.FromJson<T>()));

            return tcs.Task;
        }

        internal Task<T> PutAsync<T>(string url, object obj, params Parameter[] parameters) where T : new() {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.PUT, obj, parameters), response => tcs.SetResult(response.Content.FromJson<T>()));

            return tcs.Task;
        }
        #endregion
    }
}