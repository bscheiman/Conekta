#region
using System.Runtime.Serialization;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Card : BaseObject {
        [JsonProperty("Active")]
        public bool Active { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("exp_month")]
        public string ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public string ExpYear { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public static explicit operator Card(string cardId) {
            return new Card {
                Id = cardId
            };
        }
    }
}