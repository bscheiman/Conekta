#region
using System;
using NUnit.Framework;

#endregion

namespace Conekta.Tests {
    [TestFixture]
    internal class ConstructorTests {
        [Test]
        public void TestEmptyConstructor() {
            Environment.SetEnvironmentVariable(ConektaLib.EnvironmentKey, "");

            Assert.Throws<InvalidKeyException>(() => new ConektaLib());
            Assert.Throws<InvalidKeyException>(() => new ConektaLib(string.Empty));
            Assert.DoesNotThrow(() => new ConektaLib("blah"));

            Environment.SetEnvironmentVariable(ConektaLib.EnvironmentKey, "blah");
            Assert.DoesNotThrow(() => new ConektaLib());
            Assert.DoesNotThrow(() => new ConektaLib(string.Empty));
        }
    }
}
