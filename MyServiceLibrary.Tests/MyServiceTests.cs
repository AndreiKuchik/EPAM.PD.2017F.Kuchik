using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.CreateId;

namespace MyServiceLibrary.Tests
{
    [TestClass]
    public class MyServiceTests
    {
        [TestMethod]
         public void Add_NullUser()
        {
            User user = null;

            MyService service = new MyService();
            bool result = service.Add(user);

            Assert.AreEqual(false,result);
        }

        [TestMethod]
        public void Add_ExistUser()
        {
            User user = new User("Name","Surname");
            User user1 = new User("Name1", "Surname1");
            
            MyService service = new MyService(new CreateFirstMethodId());
            service.Add(user);
            service.Add(user1);
            bool result = service.Add(user);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Remove_NullUser()
        {
            User user = null;

            MyService service = new MyService();
            bool result = service.Remove(user);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Remove_NotExistUser()
        {
            User user = new User("Name", "Surname");
            User user1 = new User("Name1", "Surname1");
            User user2 = new User("Name2", "Surname2");

            MyService service = new MyService(new CreateFirstMethodId());
            service.Add(user);
            service.Add(user1);
            bool result = service.Remove(user2);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Remove_ExistUser()
        {
            User user = new User("Name", "Surname");
            User user1 = new User("Name1", "Surname1");
            User user2 = new User("Name2", "Surname2");

            MyService service = new MyService(new CreateFirstMethodId());
            service.Add(user);
            service.Add(user1);
            service.Add(user2);
            bool result = service.Remove(user2);
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void SearchByName_NullUser()
        {
            string user = null;

            MyService service = new MyService();
            IEnumerable<User> result = service.SearchUser(service.SearchByName, user);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void SearchByName_NullMethods()
        {
            User user = new User("Name", "Surname");

            MyService service = new MyService(new CreateFirstMethodId());
            service.Add(user);
            IEnumerable<User> result = service.SearchUser(null, user.FirstName);
            Assert.AreEqual(null, result);
        }
        
    }
}
