#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class RefundDetail : BaseObject {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }
    }
}