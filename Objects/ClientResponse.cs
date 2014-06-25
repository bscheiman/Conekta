#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class ClientResponse : BaseObject {
        [DataMember(Name = "cards")]
        public Card[] Cards { get; set; }

        [DataMember(Name = "default_card")]
        public string DefaultCard { get; set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }
    }
}