using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using KE03_INTDEV_SE_2_Base.Models;


namespace DataAccessLayer.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly MatrixIncDbContext _context;

        public GebruikerRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public Gebruiker? GetByLogin(string gebruikersnaam, string wachtwoord)
        {
            return _context.Gebruikers
                .FirstOrDefault(g => g.Gebruikersnaam == gebruikersnaam && g.Wachtwoord == wachtwoord);
        }

        public void Add(Gebruiker gebruiker)
        {
            _context.Gebruikers.Add(gebruiker);
            _context.SaveChanges();
        }
    }
}
