using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLNPrintMonitor.Persistence;
using CLNPrintMonitor.Model;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitCLNPrintMonitor
{
    [TestClass]
    public class RepositoryTest
    {

        public RepositoryTest()
        {
            RepositoryTest.rep = Repository.GetInstance;
        }

        private static Repository rep;
        private const string ipv4 = "143.54.196.241";
        private TestContext testContextInstance;

        public static Repository Rep { get => rep; set => rep = value; }
        public static string Ipv4 => ipv4;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        
        [TestMethod]
        public void TestAdd()
        {
            Printer printer = new Printer("UnitTest", IPAddress.Parse(RepositoryTest.Ipv4));
            bool result = Task.Run<bool>(async () =>
            {
                return await RepositoryTest.Rep.Add(printer);
            }).GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelect()
        {
            Printer printer = Task.Run<Printer>(async () =>
            {
                return await RepositoryTest.Rep.Select(RepositoryTest.Ipv4);
            }).GetAwaiter().GetResult();
            Assert.AreEqual(printer.Name, "UnitTest");
            Console.WriteLine("Nome da impressora: " + printer.Name);
            Console.WriteLine("Endereço da impressora: " + printer.Address);
        }

        [TestMethod]
        public void TestSelectId()
        {
            int id = Task.Run<int>(async () =>
            {
                return await RepositoryTest.Rep.SelectId(RepositoryTest.Ipv4);
            }).GetAwaiter().GetResult();
            Assert.AreNotSame(0, id);
            Console.WriteLine("Id recuperado: " + id);
        }

        [TestMethod]
        public void TestSelectAll()
        {
            List<Printer> printers = Task.Run<List<Printer>>(async () => { 
                return await RepositoryTest.Rep.SelectAll();
            }).GetAwaiter().GetResult();
            Assert.IsNotNull(printers);
            foreach(Printer printer in printers)
            {
                Console.WriteLine("Nome da impressora: " + printer.Name);
                Console.WriteLine("Endereço da impressora: " + printer.Address);
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            int id = Task.Run<int>(async () =>
            {
                return await RepositoryTest.Rep.SelectId(RepositoryTest.Ipv4);
            }).GetAwaiter().GetResult();
            Printer printer = new Printer("UnitTestChanged", IPAddress.Parse(RepositoryTest.Ipv4));
            bool result = Task.Run<bool>(async () =>
            {
                return await RepositoryTest.Rep.Update(id, printer);
            }).GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRemove()
        {
            bool result = Task.Run<bool>(async () =>
            {
                return await RepositoryTest.Rep.Remove(RepositoryTest.Ipv4);
            }).GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

    }
}
