#region
using System;
using System.ComponentModel;

#endregion

namespace Conekta.Objects {
    [Flags]
    public enum Event {
        [Description("charge.paid")] ChargePaid = 1 << 1,
        [Description("charge.under_fraud_review")] ChargeUnderFraudReview = 1 << 2,
        [Description("charge.fraudulent")] ChargeFraudulent = 1 << 3,
        [Description("charge.refunded")] ChargeRefunded = 1 << 4,
        [Description("charge.created")] ChargeCreated = 1 << 5,
        [Description("customer.created")] CustomerCreated = 1 << 6,
        [Description("customer.updated")] CustomerUpdated = 1 << 7,
        [Description("customer.deleted")] CustomerDeleted = 1 << 8,
        [Description("webhook.created")] WebhookCreated = 1 << 9,
        [Description("webhook.updated")] WebhookUpdated = 1 << 10,
        [Description("webhook.deleted")] WebhookDeleted = 1 << 11,
        [Description("charge.chargeback.created")] ChargeChargebackCreated = 1 << 12,
        [Description("charge.chargeback.updated")] ChargeChargebackUpdated = 1 << 13,
        [Description("charge.chargeback.under_review")] ChargeChargebackUnderReview = 1 << 14,
        [Description("charge.chargeback.lost")] ChargeChargebackLost = 1 << 15,
        [Description("charge.chargeback.won")] ChargeChargebackWon = 1 << 16,
        [Description("payout.created")] PayoutCreated = 1 << 17,
        [Description("payout.retrying")] PayoutRetrying = 1 << 18,
        [Description("payout.paid_out")] PayoutPaidOut = 1 << 19,
        [Description("payout.failed")] PayoutFailed = 1 << 20,
        [Description("plan.created")] PlanCreated = 1 << 21,
        [Description("plan.updated")] PlanUpdated = 1 << 22,
        [Description("plan.deleted")] PlanDeleted = 1 << 23,
        [Description("subscription.created")] SubscriptionCreated = 1 << 24,
        [Description("subscription.paused")] SubscriptionPaused = 1 << 25,
        [Description("subscription.resumed")] SubscriptionResumed = 1 << 26,
        [Description("subscription.canceled")] SubscriptionCanceled = 1 << 27,
        [Description("subscription.expired")] SubscriptionExpired = 1 << 28,
        [Description("subscription.updated")] SubscriptionUpdated = 1 << 29,
        [Description("subscription.paid")] SubscriptionPaid = 1 << 30,
        [Description("subscription.payment_failed")] SubscriptionPaymentFailed = 1 << 31,
        [Description("payee.created")] PayeeCreated = 1 << 32,
        [Description("payee.updated")] PayeeUpdated = 1 << 33,
        [Description("payee.deleted")] PayeeDeleted = 1 << 34,
        [Description("payee.payout_method.created")] PayeePayoutMethodCreated = 1 << 35,
        [Description("payee.payout_method.updated")] PayeePayoutMethodUpdated = 1 << 36,
        [Description("payee.payout_method.deleted")] PayeePayoutMethodDeleted = 1 << 37,

        All =
            ChargeCreated | ChargePaid | ChargeUnderFraudReview | ChargeFraudulent | ChargeRefunded | ChargeCreated | CustomerCreated |
            CustomerUpdated | CustomerDeleted | WebhookCreated | WebhookUpdated | WebhookDeleted | ChargeChargebackCreated |
            ChargeChargebackUpdated | ChargeChargebackUnderReview | ChargeChargebackLost | ChargeChargebackWon | PayoutCreated |
            PayoutRetrying | PayoutPaidOut | PayoutFailed | PlanCreated | PlanUpdated | PlanDeleted | SubscriptionCreated |
            SubscriptionPaused | SubscriptionResumed | SubscriptionCanceled | SubscriptionExpired | SubscriptionUpdated | SubscriptionPaid |
            SubscriptionPaymentFailed | PayeeCreated | PayeeUpdated | PayeeDeleted | PayeePayoutMethodCreated | PayeePayoutMethodUpdated |
            PayeePayoutMethodDeleted
    }
}
