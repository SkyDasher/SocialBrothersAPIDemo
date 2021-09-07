using SocialBrothersAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPIDemo.Repositories
{
    public class AdresRepository : IAdresRepository
    {
        private readonly List<Adres> adressen = new()
        {
            new Adres { AdresId = Guid.NewGuid(), Huisnummer = "11a", Plaats = "Zevenaar", Land = "Nederland", Postcode = "6901 AR", Straat = "Nieuwe Doelenstraat" },
            new Adres { AdresId = Guid.NewGuid(), Huisnummer = "63a", Plaats = "Drachten", Land = "Nederland", Postcode = "9203 ZW", Straat = "Moleneind Zuidzijde" },
            new Adres { AdresId = Guid.NewGuid(), Huisnummer = "121", Plaats = "Alkmaar", Land = "Nederland", Postcode = "1823 CN", Straat = "Oosterweezenstraat" }
        };

        public void CreateAdres(Adres adres)
        {
            adressen.Add(adres);
        }

        public void DeleteAdres(Guid id)
        {
            var index = adressen.FindIndex(existingAdres => existingAdres.AdresId == id);
            adressen.RemoveAt(index);
        }

        public IEnumerable<Adres> GetAdres()
        {
            return adressen;
        }
        public Adres GetAdres(Guid AdresId)
        {
            return adressen.Where(adres => adres.AdresId == AdresId).SingleOrDefault();
        }

        public void UpdateAdres(Adres adres)
        {
            var index = adressen.FindIndex(existingAdres => existingAdres.AdresId == adres.AdresId);
            adressen[index] = adres;
        }
    }
}
