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
    }
}
