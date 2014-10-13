#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Subscription : BaseObject {
        [DataMember(Name = "billing_cycle_end")]
        public int BillingCycleEnd { get; set; }

        [DataMember(Name = "billing_cycle_start")]
        public int BillingCycleStart { get; set; }

        [DataMember(Name = "card")]
        public string Card { get; set; }

        [DataMember(Name = "plan")]
        public string Plan { get; set; }

        [DataMember(Name = "start")]
        public int Start { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}