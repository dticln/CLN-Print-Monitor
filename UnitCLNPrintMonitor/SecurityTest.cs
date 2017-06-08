using CLNPrintMonitor.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitCLNPrintMonitor
{
    [TestClass]
    public class SecurityTest
    {

        private string secret = "4wQ7wfUePocSRV1svShrhPS+TQehjPCAJOnolGCz3ARJ8zg3yeFo6LrbLW/lxkdoISwXUDPsa6RxuERWU6aezA==";

        [TestMethod]
        public void TestEncrypt()
        {
            string response = Security.Encrypt(secret);
            Assert.IsNotNull(response);
            Assert.AreNotEqual(response, string.Empty);
            string newest = Security.Decrypt(response);
            Assert.AreEqual(secret, newest);
            Console.WriteLine(response);
        }

    }
}
