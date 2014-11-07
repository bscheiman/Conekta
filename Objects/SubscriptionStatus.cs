#region
using System.ComponentModel;

#endregion

namespace Conekta.Objects {
    public enum SubscriptionStatus {
        Unknown,
        [Description("in_trial")] InTrial,
        [Description("active")] Active,
        [Description("past_due")] PastDue,
        [Description("paused")] Paused,
        [Description("canceled")] Canceled,
        [Description("pending_pause")] PendingPause,
        [Description("pending_cancelation")] PendingCancelation,
        [Description("past_due_pending_pause")] PastDuePendingPause,
        [Description("past_due_pending_cancelation")] PastDuePendingCancelation
    }
}