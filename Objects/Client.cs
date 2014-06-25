#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Client : BaseObject {
        [DataMember(Name = "billing_address")]
        public string BillingAddress { get; set; }

        [DataMember(Name = "cards")]
        public string[] Cards { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "plan")]
        public string Plan { get; set; }

        [DataMember(Name = "shipping_address")]
        public string ShippingAddress { get; set; }
    }
}