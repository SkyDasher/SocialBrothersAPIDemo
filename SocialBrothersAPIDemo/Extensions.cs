using SocialBrothersAPIDemo.DTO;
using SocialBrothersAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPIDemo
{
    public static class Extensions
    {
        public static AdresDto AsDto (this Adres adres)
        {
            return new AdresDto
            {
                AdresId = adres.AdresId,
                Straat = adres.Straat,
                Huisnummer = adres.Huisnummer,
                Postcode = adres.Postcode,
                Plaats = adres.Plaats,
                Land = adres.Land
            };
        }
    }
}
