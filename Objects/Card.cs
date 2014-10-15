#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    public class Card : BaseObject {
        [DataMember(Name = "Active")]
        public bool Active { get; set; }

        [DataMember(Name = "brand")]
        public string Brand { get; set; }

        [DataMember(Name = "customer_id")]
        public string CustomerId { get; set; }

        [DataMember(Name = "exp_month")]
        public string ExpMonth { get; set; }

        [DataMember(Name = "exp_year")]
        public string ExpYear { get; set; }

        [DataMember(Name = "last4")]
        public string Last4 { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}