#region
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class Details {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("line_items")]
        public IList<object> LineItems { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        internal Details() {
        }
    }
}