#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Charge : BaseObject {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "customer_id")]
        public string CustomerId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "details")]
        public Details Details { get; set; }

        [DataMember(Name = "failure_code")]
        public string FailureCode { get; set; }

        [DataMember(Name = "failure_message")]
        public string FailureMessage { get; set; }

        [DataMember(Name = "fee")]
        public int Fee { get; set; }

        [DataMember(Name = "livemode")]
        public bool Livemode { get; set; }

        [DataMember(Name = "monthly_installments")]
        public object MonthlyInstallments { get; set; }

        [DataMember(Name = "paid_at")]
        public int? PaidAt { get; set; }

        [DataMember(Name = "payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [DataMember(Name = "reference_id")]
        public object ReferenceId { get; set; }

        [DataMember(Name = "refunds")]
        public IList<Refund> Refunds { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        internal Charge() {
        }
    }
}