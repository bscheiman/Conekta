#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class ConektaJson {
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("livemode")]
        public bool Livemode { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("webhook_logs")]
        public List<WebhookLog> WebhookLogs { get; set; }

        [JsonProperty("webhook_status")]
        public string WebhookStatus { get; set; }
    }
}