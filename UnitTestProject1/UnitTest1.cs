using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementation;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommandLineTest()
        {
            DataHandler datahandler = new DataHandler(); //.Instance;
            CommandHandler commandHandler = new CommandHandler();

            commandHandler.HandleCommand("Neil -> Post 1", datahandler);
            commandHandler.HandleCommand("Neil -> Post 2", datahandler);
            commandHandler.HandleCommand("Jai -> Post 1", datahandler);
            commandHandler.HandleCommand("Neil -> Post 3", datahandler);
            commandHandler.HandleCommand("Ramone -> Post 1", datahandler);
            commandHandler.HandleCommand("Neil -> Post 4", datahandler);
            commandHandler.HandleCommand("Neil follows Jai", datahandler);
            commandHandler.HandleCommand("Jai -> Post 2", datahandler);
            commandHandler.HandleCommand("Neil -> Post 5", datahandler);
            commandHandler.HandleCommand("Neil -> Post 6", datahandler);
            Assert.IsTrue(datahandler.Read("Neil").Count == 6);
            Assert.IsTrue(datahandler.BuildWall("Neil").Count == 8);
            datahandler.Dispose();
        }

        [TestMethod]
        public void InsertandFindPersonTest()
        {
            DataHandler handler = new DataHandler(); //.Instance;
            handler.InsertPerson("Jai");
            handler.InsertPerson("Neil");
            handler.InsertPerson("Frank");
            handler.InsertPerson("Adrian");
            Assert.IsNotNull(handler.Find("Frank"));
        }
        [TestMethod]
        public void PostToWallTest()
        {
            DataHandler handler = new DataHandler(); //.Instance;
            handler.InsertPerson("Neil");
            handler.InsertPost("Neil","This is my first post");
            Assert.IsTrue(handler.Read("Neil").Count == 1);
        }

        [TestMethod]
        public void ReadTest()
        {
            DataHandler handler = new DataHandler(); //.Instance;
            handler.InsertPerson("Neil");
            handler.InsertPost("Neil", "post 1");
            handler.InsertPost("Neil", "post 2");
            handler.InsertPerson("Jai");
            handler.InsertPost("Jai", "This is my first post");
            handler.InsertPost("Neil", "post 3");
            handler.InsertPerson("Ramone");
            handler.InsertPost("Ramone", "This is my first post");
            handler.InsertPost("Neil", "post 5");
            Assert.IsTrue(handler.Read("Neil").Count == 4);
        }

        [TestMethod]
        public void BuildWallTest()
        {
            DataHandler handler = new DataHandler(); //.Instance;
            handler.InsertPerson("Neil");
            handler.InsertPost("Neil", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            handler.InsertPerson("Jai");
            handler.Follow("Neil", "Jai");
            handler.InsertPost("Jai", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            handler.InsertPerson("Ramone");
            handler.InsertPost("Ramone", "This is my first post");
            handler.InsertPost("Neil", "This is my first post");
            Assert.IsTrue(handler.Read("Jai").Count == 1);
            Assert.IsTrue(handler.Read("Ramone").Count == 1);
            Assert.IsTrue(handler.BuildWall("Neil").Count == 5);
            Assert.IsTrue(handler.BuildWall("Jai").Count == 1);
        }

    }
}
