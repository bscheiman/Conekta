#region
using System.ComponentModel;

#endregion

namespace Conekta.Objects {
    public enum ChargeType {
        [Description("card")] Card,
        [Description("cash")] Cash,
        [Description("bank")] Bank
    }
}