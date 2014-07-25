#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Card : BaseObject {
        [DataMember(Name = "brand")]
        public string Brand { get; internal set; }

        [DataMember(Name = "exp_month")]
        public string ExpMonth { get; internal set; }

        [DataMember(Name = "exp_year")]
        public string ExpYear { get; internal set; }

        [DataMember(Name = "last4")]
        public string Last4 { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        internal Card() {
        }
    }
}