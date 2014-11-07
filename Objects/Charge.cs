#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Charge : BaseObject {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("failure_code")]
        public string FailureCode { get; set; }

        [JsonProperty("failure_message")]
        public string FailureMessage { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("monthly_installments")]
        public object MonthlyInstallments { get; set; }

        [JsonProperty("paid_at")]
        public int? PaidAt { get; set; }

        [JsonProperty("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty("reference_id")]
        public object ReferenceId { get; set; }

        [JsonProperty("refunds")]
        public IList<Refund> Refunds { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public static explicit operator Charge(string chargeId) {
            return new Charge {
                Id = chargeId
            };
        }
    }
}