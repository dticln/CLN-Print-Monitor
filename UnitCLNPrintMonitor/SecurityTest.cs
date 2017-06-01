using CLNPrintMonitor.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitCLNPrintMonitor
{
    [TestClass]
    public class SecurityTest
    {

        private string secret = "teste";

        [TestMethod]
        public void TestEncrypt()
        {
            string response = Security.Encrypt(secret);
            Assert.IsNotNull(response);
            Assert.AreNotEqual(response, string.Empty);
            string newest = Security.Decrypt(response);
            Assert.AreEqual(secret, newest);
        }

    }
}
