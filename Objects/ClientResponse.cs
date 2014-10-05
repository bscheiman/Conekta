#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class ClientResponse {
        // TODO: FIX/Refactor
        [DataMember(Name = "billing_address")]
        public string BillingAddress { get; internal set; }

        [DataMember(Name = "cards")]
        public Card[] Cards { get; set; }

        [DataMember(Name = "default_card")]
        public string DefaultCard { get; internal set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; internal set; }

        [DataMember(Name = "email")]
        public string Email { get; internal set; }

        public static ClientResponse Empty {
            get {
                return new ClientResponse {
                    Subscription = new Subscription {
                        BillingCycleEnd = 0,
                        BillingCycleStart = 0
                    }
                };
            }
        }

        [DataMember(Name = "livemode")]
        public bool Livemode { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "phone")]
        public string Phone { get; internal set; }

        [DataMember(Name = "plan")]
        public string Plan { get; internal set; }

        [DataMember(Name = "shipping_address")]
        public string ShippingAddress { get; internal set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        internal ClientResponse() {
        }
    }
}