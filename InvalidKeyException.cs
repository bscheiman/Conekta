#region
using System;

#endregion

namespace Conekta {
    public class InvalidKeyException : Exception {
        public InvalidKeyException(string s) : base(s) {
        }
    }
}