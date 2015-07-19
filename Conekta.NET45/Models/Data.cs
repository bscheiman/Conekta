#region
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class Data {
        [JsonProperty("object")]
        public ConektaObject Object { get; set; }

        [JsonProperty("previous_attributes")]
        public PreviousAttributes PreviousAttributes { get; set; }
    }
}