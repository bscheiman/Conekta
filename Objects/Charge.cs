#region
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace Conekta.Objects {
    [DataContract]
    public class Refund : BaseObject {
        [DataMember(Name = "amount")]
        public int Amount { get; internal set; }

        [DataMember(Name = "auth_code")]
        public string AuthCode { get; internal set; }

        internal Refund() {
        }
    }

    [DataContract]
    public class PaymentMethod : Card {
        [DataMember(Name = "auth_code")]
        public string AuthCode { get; internal set; }

        internal PaymentMethod() {
        }
    }

    [DataContract]
    public class Details {
        [DataMember(Name = "email")]
        public string Email { get; internal set; }

        [DataMember(Name = "line_items")]
        public IList<object> LineItems { get; internal set; }

        [DataMember(Name = "name")]
        public string Name { get; internal set; }

        [DataMember(Name = "phone")]
        public object Phone { get; internal set; }

        internal Details() {
        }
    }

    [DataContract]
    public class Charge : BaseObject {
        [DataMember(Name = "amount")]
        public int Amount { get; internal set; }

        [DataMember(Name = "code")]
        public string Code { get; internal set; }

        [DataMember(Name = "currency")]
        public string Currency { get; internal set; }

        [DataMember(Name = "customer_id")]
        public string CustomerId { get; internal set; }

        [DataMember(Name = "description")]
        public string Description { get; internal set; }

        [DataMember(Name = "details")]
        public Details Details { get; internal set; }

        [DataMember(Name = "failure_code")]
        public string FailureCode { get; internal set; }

        [DataMember(Name = "failure_message")]
        public string FailureMessage { get; internal set; }

        [DataMember(Name = "fee")]
        public int Fee { get; internal set; }

        [DataMember(Name = "livemode")]
        public bool Livemode { get; internal set; }

        [DataMember(Name = "monthly_installments")]
        public object MonthlyInstallments { get; internal set; }

        [DataMember(Name = "paid_at")]
        public int? PaidAt { get; internal set; }

        [DataMember(Name = "payment_method")]
        public PaymentMethod PaymentMethod { get; internal set; }

        [DataMember(Name = "reference_id")]
        public object ReferenceId { get; internal set; }

        [DataMember(Name = "refunds")]
        public IList<Refund> Refunds { get; internal set; }

        [DataMember(Name = "status")]
        public string Status { get; internal set; }

        internal Charge() {
        }
    }
}