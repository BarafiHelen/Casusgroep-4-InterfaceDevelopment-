using System.ComponentModel.DataAnnotations;
using KE03_INTDEV_SE_2_Base.Models;
using DataAccessLayer.Interfaces;

namespace KE03_INTDEV_SE_2_Base.Models
{
    public class Gebruiker
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Gebruikersnaam { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }

    }
}

