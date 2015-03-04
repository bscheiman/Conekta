#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Subscription : BaseObject {
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("billing_cycle_end")]
        public int? BillingCycleEnd { get; set; }

        [JsonProperty("billing_cycle_start")]
        public int? BillingCycleStart { get; set; }

        [JsonProperty("card")]
        public string Card { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("expiry_count")]
        public int? ExpiryCount { get; set; }

        [JsonProperty("frequency")]
        public int? Frequency { get; set; }

        [JsonProperty("interval")]
        public Interval Interval { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("start")]
        public int? Start { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("trial_period_days")]
        public int? TrialPeriodDays { get; set; }

        public static implicit operator Subscription(string subId) {
            return new Subscription {
                Id = subId
            };
        }
    }
}