using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Interfaces;
using KE03_INTDEV_SE_2_Base.Models;


namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class GebruikerController : Controller
    {
        private readonly IGebruikerRepository _repo;

        public GebruikerController(IGebruikerRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Registreer()
        {
            return View(new Gebruiker());
        }


        [HttpPost]
        public IActionResult Registreer(Gebruiker gebruiker)
        {
            if (!ModelState.IsValid) return View(gebruiker);

            _repo.Add(gebruiker);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View(new Gebruiker());
        }


        [HttpPost]
        public IActionResult Login(Gebruiker gebruiker)
        {
            var gevonden = _repo.GetByLogin(gebruiker.Gebruikersnaam, gebruiker.Wachtwoord);
            if (gevonden != null)
            {
                HttpContext.Session.SetString("Gebruiker", gevonden.Gebruikersnaam);
                return RedirectToAction("Profiel");
            }

            ViewBag.Melding = "Foute login";
            return View();
        }

        public IActionResult Profiel()
        {
            var naam = HttpContext.Session.GetString("Gebruiker");
            if (string.IsNullOrEmpty(naam))
                return RedirectToAction("Login");

            ViewBag.Gebruikersnaam = naam;
            return View();
        }

        public IActionResult Uitloggen()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}