using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientsControllerTest : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }

        public ClientsControllerTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=abel_trotter_test;";
        }

        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Index = controller.Index();
            Assert.IsInstanceOfType(Index, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_HasCorrectModelType_Account()
        {
            ViewResult Index = new ClientsController().Index() as ViewResult;
            var result = Index.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Client>));
        }

        [TestMethod]
        public void New_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult New = controller.New();
            Assert.IsInstanceOfType(New, typeof(ViewResult));
        }

        [TestMethod]
        public void New_HasCorrectModelType_Account()
        {
            ViewResult New = new ClientsController().New() as ViewResult;
            var result = New.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(List<Stylist>));
        }

        [TestMethod]
        public void Create_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Create = controller.Create("Mary Sue", "555-555-5555", "test@test.com", 1);
            Assert.IsInstanceOfType(Create, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Show_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Show = controller.Show(1);
            Assert.IsInstanceOfType(Show, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_HasCorrectModelType_Account()
        {
            ViewResult Show = new ClientsController().Show(1) as ViewResult;
            var result = Show.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Client));
        }

        [TestMethod]
        public void Edit_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Edit = controller.Edit(1);
            Assert.IsInstanceOfType(Edit, typeof(ViewResult));
        }

        [TestMethod]
        public void Edit_HasCorrectModelType_Account()
        {
            ViewResult Edit = new ClientsController().Edit(1) as ViewResult;
            var result = Edit.ViewData.Model;
            Assert.IsInstanceOfType(result, typeof(Client));
        }

        [TestMethod]
        public void Update_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Update = controller.Update("John Doe", "444-444-4444", "test@test.com", 2, 1);
            Assert.IsInstanceOfType(Update, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Delete_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult Delete = controller.Delete(1);
            Assert.IsInstanceOfType(Delete, typeof(RedirectToActionResult));
        }
    }
}
