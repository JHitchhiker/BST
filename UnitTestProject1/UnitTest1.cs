using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementation;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InsertandFindPersonTest()
        {
            DataHandler handler = DataHandler.Instance;
            handler.InsertPerson("Jai");
            handler.InsertPerson("Neil");
            handler.InsertPerson("Frank");
            handler.InsertPerson("Adrian");
            Assert.IsNotNull(handler.Find("Frank"));
        }
        [TestMethod]
        public void PostToWallTest()
        {
            DataHandler handler = DataHandler.Instance;
            handler.InsertPerson("Neil");
            handler.InsertPost("Neil","This is my first post");
            Assert.IsTrue(handler.Read("Neil").Count == 1);
        }

        [TestMethod]
        public void CommandLineTest()
        {
            DataHandler handler = DataHandler.Instance;
            CommandHandler commandHandler = new CommandHandler();
            commandHandler.HandleCommand("Neil -> First Post", handler);
            handler.InsertPost("Neil", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            handler.InsertPerson("Jai");
            handler.InsertPost("Jai", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            
            handler.InsertPost("Ramone", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            Assert.IsTrue(handler.Read("Neil").Count == 4);
        }
    }
}
