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

        [DataMember(Name = "livemode")]
        public bool Livemode { get; internal set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        internal ClientResponse() {
        }
    }
}