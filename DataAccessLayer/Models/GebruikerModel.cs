using System.ComponentModel.DataAnnotations;
using KE03_INTDEV_SE_2_Base.Models;
using DataAccessLayer.Interfaces;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
    }


}

