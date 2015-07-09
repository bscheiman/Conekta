#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using bscheiman.Common.Extensions;
using Conekta.Objects;
using RestSharp;

#endregion

namespace Conekta {
    /// <summary>
    ///     Wrapper .NET para el API REST de Conekta.io.
    ///     TODOS los métodos son async. Es necesaria una leída previa a los docs de Conekta.
    /// </summary>
    public class ConektaLib {
        private const string AppHeader = "application/vnd.conekta-v1.0.0+json";
        private const string BaseUrl = "https://api.conekta.io/";
        internal string PrivateKey { get; set; }

        /// <summary>
        ///     Genera una instancia del wrapper
        /// </summary>
        /// <param name="key">Llave PRIVADA. Checa el sitio de administración de Conekta.</param>
        public ConektaLib(string key = "") {
            var apiKey = Environment.GetEnvironmentVariable("CONEKTA_API_KEY");

            if (string.IsNullOrEmpty(key) && string.IsNullOrEmpty(apiKey))
                throw new InvalidKeyException("PrivateKey hasn't been set.");

            if (string.IsNullOrEmpty(key))
                key = apiKey;

            PrivateKey = key;
        }

        /// <summary>
        ///     Agrega una tarjeta al cliente especificado en client. Puede ser un objeto o un string id (id de Conekta)
        /// </summary>
        /// <param name="client">Objeto Client o string con el id</param>
        /// <param name="tokenId">Card token</param>
        /// <returns>Card</returns>
        public Task<Card> AddCardAsync(Client client, string tokenId) {
            return PostAsync<Card>("customers/{clientId}/cards/", new {
                token = tokenId
            }, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Agrega un webhook en produccion/desarrollo
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="events">Eventos a monitorear. Usa Events.All para todos.</param>
        /// <returns>Webhook</returns>
        public Task<Webhook> AddWebhookAsync(string url, Event events) {
            var flags = Enum.GetValues(events.GetType()).Cast<Enum>().Where(events.HasFlag).Cast<Event>().ToArray();
            var list = (from f in flags
                        select f.GetAttributeOfType<DescriptionAttribute>()
                        into description
                        where description != null
                        select description.Description).ToList();

            return PostAsync<Webhook>("webhooks", new {
                url,
                events = list.ToArray()
            });
        }

        /// <summary>
        ///     Hace un cargo a la tarjeta especificada.
        /// </summary>
        /// <param name="card">Objeto card o string con el id</param>
        /// <param name="amount">Monto en pesos (1 = $1.00); internamente se multiplica por 100</param>
        /// <param name="currency">Moneda [USD/MXN]</param>
        /// <param name="desc">Descripción del cargo</param>
        /// <param name="details">Hash/diccionario opcional</param>
        /// <returns>Charge</returns>
        public Task<Charge> ChargeAsync(Card card, float amount, string currency, string desc, Dictionary<string, object> details = null) {
            return PostAsync<Charge>("charges", new {
                description = desc,
                amount = (amount * 100),
                currency,
                card = card.Id,
                details = details ?? new Dictionary<string, object>()
            });
        }

        /// <summary>
        ///     Genera un cargo en OXXO
        /// </summary>
        /// <param name="amount">Monto</param>
        /// <param name="currency">Moneda</param>
        /// <param name="desc">Descripción</param>
        /// <param name="expires">Fecha limite del cargo</param>
        /// <returns>Objeto charge</returns>
        public Task<Charge> ChargeOxxoAsync(float amount, string currency, string desc, DateTime expires) {
            return PostAsync<Charge>("charges", new {
                description = desc,
                amount = (amount * 100),
                currency,
                cash = new {
                    type = "oxxo",
                    expires_at = expires.ToEpoch()
                }
            });
        }

        /// <summary>
        ///     Crea un cliente
        /// </summary>
        /// <param name="name">Nombre completo</param>
        /// <param name="email">Correo</param>
        /// <param name="phone">Teléfono (opcional)</param>
        /// <param name="cards">Tarjetas (opcional)</param>
        /// <param name="planId">Plan (opcional)</param>
        /// <param name="billingAddress">Dirección (opcional)</param>
        /// <param name="shippingAddress">Dirección de envió (opcional)</param>
        /// <param name="rfc">RFC (opcional)</param>
        /// <returns>Client</returns>
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

        /// <summary>
        ///     Crea una suscripcion / pago recurrente
        /// </summary>
        /// <param name="planId">Id del plan</param>
        /// <param name="name">Nombre</param>
        /// <param name="amount">Monto en pesos (1 = $1.00)</param>
        /// <param name="currency">Moneda [USD/MXN]</param>
        /// <param name="interval">Intervalo de cargo</param>
        /// <param name="trial">Días de prueba</param>
        /// <param name="frequency">Frecuencia de cargo</param>
        /// <param name="expiry">Validez</param>
        /// <returns>Subscription</returns>
        public async Task<Subscription> CreateSubscriptionAsync(string planId, string name, float amount, string currency = "MXN",
                                                                Interval interval = Interval.Month, int trial = 7, int frequency = 1, int expiry = 0) {
            if (await SubscriptionExists(planId)) {
                return new Subscription {
                    Id = planId
                };
            }

            return await PostAsync<Subscription>("plans", new {
                id = planId,
                name,
                amount = (int) (amount * 100),
                currency,
                interval = interval.GetDescription(),
                frequency,
                trial_period_days = trial,
                expiry_count = expiry
            });
        }

        /// <summary>
        ///     Borra una tarjeta
        /// </summary>
        /// <param name="client">Cliente (objeto o string con id)</param>
        /// <param name="card">Tarjeta (objeto o string con id)</param>
        /// <returns>Card</returns>
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

        /// <summary>
        ///     Borra un cliente
        /// </summary>
        /// <param name="client">Cliente (objeto u string con id)</param>
        /// <returns>Client</returns>
        public Task<Client> DeleteClientAsync(Client client) {
            return DeleteAsync<Client>("customers/{clientId}", new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Borra un webhook
        /// </summary>
        /// <param name="hook">Hook (hook con id, o string)</param>
        /// <returns>Webhook</returns>
        public Task<Webhook> DeleteWebhookAsync(Webhook hook) {
            return DeleteAsync<Webhook>("webhooks/{hookId}", new Parameter {
                Name = "hookId",
                Value = hook.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Regresa todas las tarjetas de un cliente
        /// </summary>
        /// <param name="client">Cliente (objeto u string con id)</param>
        /// <returns>Card[]</returns>
        public Task<Card[]> GetAllCardsAsync(Client client) {
            return GetAsync<Card[]>("customers/{clientId}/cards", null, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Regresa todos los cargos de un cliente
        /// </summary>
        /// <returns>Charge[]</returns>
        public Task<Charge[]> GetAllChargesAsync() {
            return GetAsync<Charge[]>("charges");
        }

        /// <summary>
        ///     Regresa todos los clientes disponibles
        /// </summary>
        /// <returns>Client</returns>
        public Task<Client[]> GetAllClientsAsync() {
            return GetAsync<Client[]>("customers");
        }

        /// <summary>
        ///     Regresa todos los hooks disponibles
        /// </summary>
        /// <returns>Webhook[]</returns>
        public Task<Webhook[]> GetAllWebhooksAsync() {
            return GetAsync<Webhook[]>("webhooks");
        }

        /// <summary>
        ///     Regresa información de un cliente
        /// </summary>
        /// <param name="clientId">Cliente (string con id)</param>
        /// <returns>Client</returns>
        public Task<Client> GetClientAsync(string clientId) {
            return GetAsync<Client>("customers/{customerId}", null, new Parameter {
                Name = "customerId",
                Value = clientId,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Regresa información de una suscripción
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public Task<Subscription> GetSubscriptionAsync(string planId) {
            return GetAsync<Subscription>("plans/{planId}", null, new Parameter {
                Name = "planId",
                Value = planId,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Regresa información de todas las suscripciones que existen
        /// </summary>
        /// <returns>Suscription[]</returns>
        public Task<Subscription[]> GetSubscriptionsAsync() {
            return GetAsync<Subscription[]>("plans");
        }

        /// <summary>
        ///     Genera una devolución TOTAL del cargo especificado
        /// </summary>
        /// <param name="charge">Cargo a devolver (objeto o string con id)</param>
        /// <returns>Refund</returns>
        public Task<Refund> RefundAsync(Charge charge) {
            return PostAsync<Refund>("charges/{chargeId}/refund", null, new Parameter {
                Name = "chargeId",
                Value = charge.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Actualiza la suscripción para el cliente
        /// </summary>
        /// <param name="client">Cliente (objeto o string con id)</param>
        /// <param name="planId">Suscripción nueva</param>
        /// <returns></returns>
        public Task<Subscription> SetSubscriptionForClientAsync(Client client, string planId) {
            return PostAsync<Subscription>("customers/{clientId}/subscription", new {
                plan = planId
            }, new Parameter {
                Name = "clientId",
                Value = client.Id,
                Type = ParameterType.UrlSegment
            });
        }

        /// <summary>
        ///     Actualiza el status de la suscripción para un cliente
        /// </summary>
        /// <param name="client">Cliente (objeto o string con id)</param>
        /// <param name="status">Estatus</param>
        /// <param name="planId">Suscripción nueva</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Hace un cargo a la tarjeta especificada.
        /// </summary>
        /// <param name="amount">Monto en pesos (1 = $1.00); internamente se multiplica por 100</param>
        /// <param name="currency">Moneda [USD/MXN]</param>
        /// <param name="desc">Descripción del cargo</param>
        /// <returns>Charge</returns>
        public Task<Charge> SpeiChargeAsync(float amount, string currency, string desc) {
            return PostAsync<Charge>("charges", new {
                description = desc,
                amount = (amount * 100),
                currency,
                bank = new {
                    type = "spei"
                }
            });
        }

        /// <summary>
        ///     Cambia la tarjeta default de un usuario. Ojo, esto borra todas las demas tarjetas.
        /// </summary>
        /// <param name="client">Cliente (objeto o string con id)</param>
        /// <param name="cardToken">Token de la tarjeta</param>
        /// <returns></returns>
        public async Task<Card> SwitchCardAsync(Client client, string cardToken) {
            if (client.Cards == null)
                client = await GetClientAsync(client.Id);

            foreach (var card in client.Cards)
                await DeleteCardAsync(client, card);

            return await AddCardAsync(client, cardToken);
        }

        /// <summary>
        ///     Prueba si una llave es válida o no.
        /// </summary>
        /// <param name="key">Llave a probar</param>
        /// <returns>true/false</returns>
        public static async Task<bool> TestKey(string key) {
            try {
                await new ConektaLib(key).GetAsync<Client>("customers/client_noop");

                return true;
            } catch (InvalidKeyException) {
                return false;
            } catch {
                return true;
            }
        }

        /// <summary>
        ///     Prueba si la llave actual es válida o no.
        /// </summary>
        /// <returns>true/false</returns>
        public Task<bool> TestKey() {
            return TestKey(PrivateKey);
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

            client.ExecuteAsync(GetRequest(url, Method.GET, obj, parameters), response => {
                var str = response.Content;
                var baseObject = typeof (T).IsArray ? str.FromJson<BaseObject[]>()[0] : str.FromJson<BaseObject>();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    tcs.SetException(new InvalidKeyException(baseObject.Message));
                else if (response.StatusCode != HttpStatusCode.OK)
                    tcs.SetException(new WebException(baseObject.Message));
                else
                    tcs.SetResult(str.FromJson<T>());
            });

            return tcs.Task;
        }

        internal RestClient GetClient(string url) {
            var client = new RestClient(BaseUrl) {
                Authenticator = new HttpBasicAuthenticator(PrivateKey, ""),
                UserAgent = "Conekta.NET"
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