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

        [TestMethod]
        public void Show_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Show = controller.Show(0);
            Assert.IsInstanceOfType(Show, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_HasCorrectModelType_Account()
        {
            ViewResult Show = new StylistsController().Show(0) as ViewResult;
            var result = Show.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Stylist));
        }

        [TestMethod]
        public void Edit_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Edit = controller.Edit(0);
            Assert.IsInstanceOfType(Edit, typeof(ViewResult));
        }

        [TestMethod]
        public void Edit_HasCorrectModelType_Account()
        {
            ViewResult Edit = new StylistsController().Show(0) as ViewResult;
            var result = Edit.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Stylist));
        }

        [TestMethod]
        public void Update_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Update = controller.Update(1, "John Doe");
            Assert.IsInstanceOfType(Update, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Delete_ReturnsCorrectView_True()
        {
            StylistsController controller = new StylistsController();
            ActionResult Delete = controller.Delete(1);
            Assert.IsInstanceOfType(Delete, typeof(RedirectToActionResult));
        }
    }
}
