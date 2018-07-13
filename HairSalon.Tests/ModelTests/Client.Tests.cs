using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }

        public ClientTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=abel_trotter_test;";
        }

        [TestMethod]
        public void SetGetProperties_SetsGetsProperties_True()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1, 1);
            client.Name = "Ygritte";
            client.Phone = "666-666-6666";
            client.Email = "test2@test.com";
            client.StylistId = 2;
            client.Id = 2;
            Assert.AreEqual("Ygritte", client.Name);
            Assert.AreEqual("666-666-6666", client.Phone);
            Assert.AreEqual("test2@test.com", client.Email);
            Assert.AreEqual(2, client.StylistId);
            Assert.AreEqual(2, client.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreTheSame_True()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1);
            Client clientTwo = new Client("Min", "555-555-5555", "test@test.com", 1);
            Assert.AreEqual(client, clientTwo);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesClientToDb_True()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1);
            client.Save();
            List<Client> allClients = Client.GetAll();
            List<Client> expectedList = new List<Client>() { client };
            CollectionAssert.AreEqual(expectedList, allClients);
        }

        [TestMethod]
        public void Update_UpdatesColumnInDatabase_ClientList()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1);
            client.Save();
            client.Name = "Egwene";
            client.Update();
            List<Client> allClients = Client.GetAll();
            List<Client> expectedList = new List<Client>{ client };
            CollectionAssert.AreEqual(expectedList, allClients);
        }

        [TestMethod]
        public void Delete_DeletesRecordFromDatabase_ClientList()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1);
            Client client2 = new Client("Egwene", "555-555-5556", "test2@test.com", 2);
            client.Save();
            client2.Save();
            client.Delete();
            List<Client> allClients = Client.GetAll();
            List<Client> expectedList = new List<Client>{ client2 };
            CollectionAssert.AreEqual(expectedList, allClients);
        }

        [TestMethod]
        public void Find_FindsClientInDatabaseById_Client()
        {
            Client client = new Client("Min", "555-555-5555", "test@test.com", 1);
            client.Save();
            Client foundClient = Client.Find(client.Id);
            Assert.AreEqual(client, foundClient);
        }
    }
}
