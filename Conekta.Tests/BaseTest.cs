#region
using System;

#endregion

namespace Conekta.Tests {
    public class BaseTest {
        protected string ApiKey {
            get { return Environment.GetEnvironmentVariable("CONEKTA_API_KEY"); }
        }
    }
}