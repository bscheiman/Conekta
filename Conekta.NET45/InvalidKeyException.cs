#region
using System;

#endregion

namespace Conekta {
    internal class InvalidKeyException : Exception {
        internal InvalidKeyException(string s) : base(s) {
        }
    }
}
