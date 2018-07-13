using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest
    {
        public void Dispose()
        {
            //Stylist.DeleteAll();
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
    }
}
