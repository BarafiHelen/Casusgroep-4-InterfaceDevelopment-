using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KE03_INTDEV_SE_2_Base.Models;


namespace DataAccessLayer.Interfaces
{
    public interface IGebruikerRepository
    {
        Gebruiker? GetByLogin(string gebruikersnaam, string wachtwoord);
        void Add(Gebruiker gebruiker);
    }
}
