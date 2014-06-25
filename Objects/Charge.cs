#region
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Charge : BaseObject {
        [DataMember(Name = "amount")]
        public string Amount { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "failure_code")]
        public string FailureCode { get; set; }

        [DataMember(Name = "failure_message")]
        public string FailureMessage { get; set; }

        [DataMember(Name = "reference_id")]
        public string ReferenceId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}