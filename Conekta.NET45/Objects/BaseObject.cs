#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects
{
    public class BaseObject
    {
        [JsonProperty("created_at")]
        public int? CreatedAt { get; set; }

        [JsonProperty("expires_at")]
        public int? ExpiresAt { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        public bool IsError
        {
            get
            {
                return Object == "error";
            }
        }

        [JsonProperty("livemode")]
        public bool? LiveMode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("message_to_purchaser")]
        public string MessageToPurchaser { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        protected internal BaseObject()
        {
        }
    }
}
