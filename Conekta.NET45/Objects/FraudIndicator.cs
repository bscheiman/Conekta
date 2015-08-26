#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class FraudIndicator {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("weight")]
        public object Weight { get; set; }

        internal FraudIndicator() {
        }
    }
}
