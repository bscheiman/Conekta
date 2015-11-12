#region
using System.Collections.Generic;
using Conekta.Objects;
using Newtonsoft.Json;

#endregion

namespace Conekta.Models {
    public class ConektaWebhook : BaseObject {
        [JsonProperty("data")]
        public WebhookData Data { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("webhook_logs")]
        public IList<WebhookLog> WebhookLogs { get; set; }

        [JsonProperty("webhook_status")]
        public string WebhookStatus { get; set; }

        internal ConektaWebhook() {
        }

        public class WebhookCard : BaseObject {
            [JsonProperty("active")]
            public bool? Active { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("customer_id")]
            public string CustomerId { get; set; }

            [JsonProperty("exp_month")]
            public string ExpMonth { get; set; }

            [JsonProperty("exp_year")]
            public string ExpYear { get; set; }

            [JsonProperty("last4")]
            public string Last4 { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            internal WebhookCard() {
            }
        }

        public class WebhookData : BaseObject {
            [JsonProperty("object")]
            public new WebhookObject Object { get; set; }

            [JsonProperty("previous_attributes")]
            public PreviousAttributes PreviousAttributes { get; set; }

            internal WebhookData() {
            }
        }

        public class WebhookDetails : BaseObject {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            internal WebhookDetails() {
            }
        }

        public class WebhookFile : BaseObject {
            [JsonProperty("file_name")]
            public string FileName { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            internal WebhookFile() {
            }
        }

        public class WebhookLog : BaseObject {
            [JsonProperty("failed_attempts")]
            public int? FailedAttempts { get; set; }

            [JsonProperty("last_attempted_at")]
            public int LastAttemptedAt { get; set; }

            [JsonProperty("last_http_response_status")]
            public int? LastHttpResponseStatus { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            internal WebhookLog() {
            }
        }

        public class WebhookObject : BaseObject {
            [JsonProperty("amount")]
            public decimal? Amount { get; set; }

            [JsonProperty("billing_cycle_end")]
            public int? BillingCycleEnd { get; set; }

            [JsonProperty("billing_cycle_start")]
            public int? BillingCycleStart { get; set; }

            [JsonProperty("card_id")]
            public string CardId { get; set; }

            [JsonProperty("cards")]
            public IList<Card> Cards { get; set; }

            [JsonProperty("charge_id")]
            public string ChargeId { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("customer_id")]
            public string CustomerId { get; set; }

            [JsonProperty("default_card_id")]
            public string DefaultCardId { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("details")]
            public Details Details { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("evidence_due_by")]
            public int? EvidenceDueBy { get; set; }

            [JsonProperty("expiry_count")]
            public int? ExpiryCount { get; set; }

            [JsonProperty("failure_code")]
            public string FailureCode { get; set; }

            [JsonProperty("failure_message")]
            public string FailureMessage { get; set; }

            [JsonProperty("fee")]
            public int? Fee { get; set; }

            [JsonProperty("files")]
            public IList<WebhookFile> Files { get; set; }

            [JsonProperty("followup_status")]
            public string FollowupStatus { get; set; }

            [JsonProperty("frequency")]
            public int? Frequency { get; set; }

            [JsonProperty("interval")]
            public string Interval { get; set; }

            [JsonProperty("monthly_installments")]
            public int? MonthlyInstallments { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("note")]
            public string Note { get; set; }

            [JsonProperty("paid_at")]
            public int? PaidAt { get; set; }

            [JsonProperty("payment_method")]
            public PaymentMethod PaymentMethod { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("plan_id")]
            public string PlanId { get; set; }

            [JsonProperty("reason")]
            public string Reason { get; set; }

            [JsonProperty("reference_id")]
            public string ReferenceId { get; set; }

            [JsonProperty("response_from_client")]
            public string ResponseFromClient { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("subscription")]
            public Subscription Subscription { get; set; }

            [JsonProperty("trial_end")]
            public int? TrialEnd { get; set; }

            [JsonProperty("trial_period_days")]
            public int? TrialPeriodDays { get; set; }

            internal WebhookObject() {
            }
        }

        public class WebhookPaymentMethod : BaseObject {
            [JsonProperty("auth_code")]
            public string AuthCode { get; set; }

            [JsonProperty("bank")]
            public string Bank { get; set; }

            [JsonProperty("barcode")]
            public string Barcode { get; set; }

            [JsonProperty("barcode_url")]
            public string BarcodeUrl { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("clabe")]
            public string Clabe { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("executed_at")]
            public int? ExecutedAt { get; set; }

            [JsonProperty("exp_month")]
            public string ExpMonth { get; set; }

            [JsonProperty("exp_year")]
            public string ExpYear { get; set; }

            [JsonProperty("issuing_account_bank")]
            public string IssuingAccountBank { get; set; }

            [JsonProperty("issuing_account_holder")]
            public string IssuingAccountHolder { get; set; }

            [JsonProperty("issuing_account_number")]
            public string IssuingAccountNumber { get; set; }

            [JsonProperty("issuing_account_tax_id")]
            public string IssuingAccountTaxId { get; set; }

            [JsonProperty("last4")]
            public string Last4 { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("receiving_account_bank")]
            public string ReceivingAccountBank { get; set; }

            [JsonProperty("receiving_account_holder")]
            public string ReceivingAccountHolder { get; set; }

            [JsonProperty("receiving_account_number")]
            public string ReceivingAccountNumber { get; set; }

            [JsonProperty("receiving_account_tax_id")]
            public string ReceivingAccountTaxId { get; set; }

            [JsonProperty("reference_number")]
            public string ReferenceNumber { get; set; }

            [JsonProperty("store_name")]
            public string StoreName { get; set; }

            [JsonProperty("tracking_code")]
            public string TrackingCode { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            internal WebhookPaymentMethod() {
            }
        }

        public class WebhookPreviousAttributes : BaseObject {
            [JsonProperty("evidence_due_by")]
            public int? EvidenceDueBy { get; set; }

            [JsonProperty("payment_method")]
            public PaymentMethod PaymentMethod { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            internal WebhookPreviousAttributes() {
            }
        }

        public class WebhookSubscription : BaseObject {
            [JsonProperty("card_id")]
            public string CardId { get; set; }

            [JsonProperty("customer_id")]
            public string CustomerId { get; set; }

            [JsonProperty("plan_id")]
            public string PlanId { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("trial_end")]
            public int? TrialEnd { get; set; }

            internal WebhookSubscription() {
            }
        }
    }
}
