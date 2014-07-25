#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Client : BaseObject {
        [DataMember(Name = "billing_address")]
        public string BillingAddress { get; internal set; }

        [DataMember(Name = "cards")]
        public string[] Cards { get; internal set; }

        [DataMember(Name = "email")]
        public string Email { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "phone")]
        public string Phone { get; internal set; }

        [DataMember(Name = "plan")]
        public string Plan { get; internal set; }

        [DataMember(Name = "shipping_address")]
        public string ShippingAddress { get; internal set; }

        internal Client() {
        }
    }
}