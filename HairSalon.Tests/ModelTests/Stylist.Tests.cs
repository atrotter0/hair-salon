using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        public StylistTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=abel_trotter_test;";
        }

        [TestMethod]
        public void SetGetProperties_SetsGetsProperties_True()
        {
            Stylist stylist = new Stylist("Min");
            stylist.Name = "Ygritte";
            stylist.Id = 2;
            Assert.AreEqual("Ygritte", stylist.Name);
            Assert.AreEqual(2, stylist.Id);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfPropertiesAreTheSame_True()
        {
            Stylist stylist = new Stylist("Min");
            Stylist stylistTwo = new Stylist("Min");
            Assert.AreEqual(stylist, stylistTwo);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Stylist.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesStylistToDb_True()
        {
            Stylist stylist = new Stylist("Ygritte");
            stylist.Save();
            List<Stylist> allStylists = Stylist.GetAll();
            List<Stylist> expectedList = new List<Stylist>() { stylist };
            CollectionAssert.AreEqual(expectedList, allStylists);
        }

        [TestMethod]
        public void Update_UpdatesColumnInDatabase_StylistList()
        {
            Stylist stylist = new Stylist("Elayne", 1);
            stylist.Save();
            stylist.Name = "Egwene";
            stylist.Update();
            List<Stylist> allStylists = Stylist.GetAll();
            List<Stylist> expectedList = new List<Stylist>{ stylist };
            CollectionAssert.AreEqual(expectedList, allStylists);
        }

        [TestMethod]
        public void Delete_DeletesRecordFromDatabaseAndSetsClientId_StylistList()
        {
            Stylist stylist = new Stylist("Elayne", 1);
            Stylist stylist2 = new Stylist("Egwene", 2);
            Client client = new Client("Min", "555-555-5555", "test@test.com", stylist.Id, 1);
            stylist.Save();
            stylist2.Save();
            stylist.Delete();
            List<Stylist> allStylists = Stylist.GetAll();
            List<Stylist> expectedList = new List<Stylist>{ stylist2 };
            client = Client.Find(client.Id);
            CollectionAssert.AreEqual(expectedList, allStylists);
            Assert.AreEqual(0, client.StylistId);
        }

        [TestMethod]
        public void Find_FindsStylistInDatabaseById_Stylist()
        {
            Stylist stylist = new Stylist("Shiva", 1);
            stylist.Save();
            Stylist foundStylist = Stylist.Find(stylist.Id);
            Assert.AreEqual(stylist, foundStylist);
        }

        [TestMethod]
        public void GetClientsForStylist_ReturnsClientsForAStylist_List()
        {
            Stylist stylist = new Stylist("Tim Drake");
            stylist.Save();
            Client client = new Client("Bruce Wayne", "555-555-5555", "test@test.com", stylist.Id);
            Client client2 = new Client("Alfred", "555-555-5555", "test2@test.com", stylist.Id);
            client.Save();
            client2.Save();
            List<Client> listOfClients = stylist.GetClientsForStylist();
            List<Client> expectedList = Client.GetAll();
            CollectionAssert.AreEqual(expectedList, listOfClients);
        }
    }
}
