using SocialBrothersAPIDemo.Models;
using System;
using System.Collections.Generic;

namespace SocialBrothersAPIDemo.Repositories
{
    public interface IAdresRepository
    {
        IEnumerable<Adres> GetAdres();
        Adres GetAdres(Guid AdresId);
        void CreateAdres(Adres adres);
        void UpdateAdres(Adres adres);
        void DeleteAdres(Guid id);
    }
}