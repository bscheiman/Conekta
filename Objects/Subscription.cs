#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Subscription : BaseObject {
        [DataMember(Name = "billing_cycle_end")]
        public int BillingCycleEnd { get; internal set; }

        [DataMember(Name = "billing_cycle_start")]
        public int BillingCycleStart { get; internal set; }

        [DataMember(Name = "card")]
        public string Card { get; internal set; }

        [DataMember(Name = "plan")]
        public string Plan { get; internal set; }

        [DataMember(Name = "start")]
        public int Start { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        internal Subscription() {
        }
    }
}