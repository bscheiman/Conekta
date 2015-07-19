#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class WebhookLog {
        [JsonProperty("failed_attempts")]
        public int FailedAttempts { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("last_attempted_at")]
        public int LastAttemptedAt { get; set; }

        [JsonProperty("last_http_response_status")]
        public int LastHttpResponseStatus { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}