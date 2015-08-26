#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Objects {
    public class LineItem {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public double? Quantity { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("unit_price")]
        public decimal? UnitPrice { get; set; }

        internal LineItem() {
        }
    }
}
