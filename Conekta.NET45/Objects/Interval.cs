#region
using System.ComponentModel;

#endregion

namespace Conekta.Objects {
    public enum Interval {
        [Description("week")] Week,
        [Description("half_month")] HalfMonth,
        [Description("month")] Month,
        [Description("year")] Year
    }
}
