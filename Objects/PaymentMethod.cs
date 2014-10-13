#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class PaymentMethod : Card {
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }

        internal PaymentMethod() {
        }
    }
}