#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Refund : BaseObject {
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("failure_code")]
        public object FailureCode { get; set; }

        [JsonProperty("failure_message")]
        public object FailureMessage { get; set; }

        [JsonProperty("fee")]
        public int? Fee { get; set; }

        [JsonProperty("monthly_installments")]
        public int? MonthlyInstallments { get; set; }

        [JsonProperty("paid_at")]
        public int? PaidAt { get; set; }

        [JsonProperty("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("refunds")]
        public IList<RefundDetail> Refunds { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}