#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Webhook : BaseObject {
        [JsonProperty("development_enabled")]
        public bool DevelopmentEnabled { get; set; }

        [JsonProperty("production_enabled")]
        public bool ProductionEnabled { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("subscribed_events")]
        public IList<string> SubscribedEvents { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        internal Webhook() {
        }

        public static implicit operator Webhook(string hookId) {
            return new Webhook {
                Id = hookId
            };
        }
    }
}