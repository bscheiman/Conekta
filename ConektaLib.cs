#region
using System.Collections.Generic;
using System.Threading.Tasks;
using Conekta.Objects;
using RestSharp;
using ServiceStack.Text;

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

        public Task<Card> AddCardAsync(string clientId, string tokenId) {
            return PostAsync<Card>("customers/{clientId}/cards/", new {
                token = tokenId
            }, new Parameter {
                Name = "clientId",
                Value = clientId,
                Type = ParameterType.UrlSegment
            });
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

        public Task<Card> DeleteCardAsync(Client client, string cardId) {
            return DeleteCardAsync(client, new Card {
                Id = cardId
            });
        }

        public Task<Card> DeleteCardAsync(string clientId, string cardId) {
            return DeleteCardAsync(new Client {
                Id = clientId
            }, new Card {
                Id = cardId
            });
        }

        public Task<Client> DeleteClientAsync(Client client) {
            return DeleteAsync<Client>("customers/{clientId}", new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Client> DeleteClientAsync(string clientId) {
            return DeleteClientAsync(new Client {
                Id = clientId
            });
        }

        public Task<List<Card>> GetAllCardsAsync(Client client) {
            return GetAsync<List<Card>>("customers/{clientId}/cards", null, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<List<Client>> GetAllClientsAsync() {
            return GetAsync<List<Client>>("customers");
        }

        #region Helpers
        internal Task<T> DeleteAsync<T>(string url, params Parameter[] parameters) where T : new() {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.DELETE, null, parameters), response => tcs.SetResult(response.Content.FromJson<T>()));

            return tcs.Task;
        }

        internal Task<T> GetAsync<T>(string url, object obj = null, params Parameter[] parameters) where T : new() {
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
            client.AddDefaultHeader("Accept", AppHeader);

            return client;
        }

        internal RestRequest GetRequest(string url, Method method, object obj, params Parameter[] parameters) {
            var request = new RestRequest(url, method);

            foreach (var p in parameters)
                request.AddParameter(p);

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
        #endregion
    }
}