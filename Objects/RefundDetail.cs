#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class RefundDetail : BaseObject {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "auth_code")]
        public string AuthCode { get; set; }
    }
}