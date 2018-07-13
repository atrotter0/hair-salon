using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<Client> allclients = Client.GetAll();
            return View(allclients);
        }

        [HttpGet("/clients/new")]
        public ActionResult New()
        {
            List<Stylist> allStylists = Stylist.GetAll();
            return View(allStylists);
        }

        [HttpPost("/clients")]
        public ActionResult Create(string name, string phone, string email, int stylistId)
        {
            Client client = new Client(name, phone, email, stylistId);
            client.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Show(int id)
        {
            Client client = Client.Find(id);
            return View(client);
        }

        [HttpGet("/clients/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Client client = Client.Find(id);
            return View(client);
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult Update(string name, string phone, string email, int stylistId, int id)
        {
            Client client = Client.Find(id);
            client.Name = name;
            client.Phone = phone;
            client.Email = email;
            client.StylistId = stylistId;
            client.Update();
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Client client = Client.Find(id);
            client.Delete();
            return RedirectToAction("Index");
        }
    }
}
