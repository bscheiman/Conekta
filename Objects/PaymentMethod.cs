#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class PaymentMethod : Card {
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("barcode_url")]
        public string BarcodeUrl { get; set; }

        [JsonProperty("clabe")]
        public string Clabe { get; set; }

        [JsonProperty("fraud_indicators")]
        public IList<string> FraudIndicators { get; set; }

        [JsonProperty("fraud_score")]
        public string FraudScore { get; set; }

        [JsonProperty("receiving_account_bank")]
        public string ReceivingAccountBank { get; set; }

        [JsonProperty("receiving_account_number")]
        public string ReceivingAccountNumber { get; set; }
    }
}