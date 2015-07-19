#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class PreviousAttributes {
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("status")]
        public object Status { get; set; }
    }
}