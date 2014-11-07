#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class PaymentMethod : Card {
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }
    }
}