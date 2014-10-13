#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Client : BaseObject {
        [DataMember(Name = "cards")]
        public IList<object> Cards { get; set; }

        [DataMember(Name = "default_card_id")]
        public object DefaultCardId { get; set; }

        [DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }
    }
}