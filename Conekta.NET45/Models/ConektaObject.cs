#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class ConektaObject {
        [JsonProperty("billing_cycle_end")]
        public int BillingCycleEnd { get; set; }

        [JsonProperty("billing_cycle_start")]
        public int BillingCycleStart { get; set; }

        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}