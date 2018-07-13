using Microsoft.AspNetCore.Mvc;
using HairSalonProject.Models;

namespace HairSalonProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
