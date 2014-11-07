#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Client : BaseObject {
        [JsonProperty("cards")]
        public IList<Card> Cards { get; set; }

        [JsonProperty("default_card_id")]
        public object DefaultCardId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public static Client Empty {
            get {
                return new Client {
                    Subscription = new Subscription()
                };
            }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        public static explicit operator Client(string clientId) {
            return new Client {
                Id = clientId
            };
        }
    }
}