using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistsControllerTest : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        public StylistsControllerTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=abel_trotter_test;";
        }

        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Index = controller.Index();
            Assert.IsInstanceOfType(Index, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_HasCorrectModelType_Account()
        {
            ViewResult Index = new StylistsController().Index() as ViewResult;
            var result = Index.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Stylist>));
        }

        [TestMethod]
        public void New_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult New = controller.New();
            Assert.IsInstanceOfType(New, typeof(ViewResult));
        }

        [TestMethod]
        public void Create_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Create = controller.Create("Mary Jane");
            Assert.IsInstanceOfType(Create, typeof(RedirectToActionResult));
        }
    }
}
