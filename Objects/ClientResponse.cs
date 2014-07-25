#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class ClientResponse : Client {
        [DataMember(Name = "cards")]
        public new Card[] Cards { get; set; }

        [DataMember(Name = "default_card")]
        public string DefaultCard { get; internal set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; internal set; }

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

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        internal ClientResponse() {
        }
    }
}