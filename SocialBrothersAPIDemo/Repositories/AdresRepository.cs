using Microsoft.Data.Sqlite;
using SocialBrothersAPIDemo.Database;
using SocialBrothersAPIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SocialBrothersAPIDemo.Repositories
{
    public class AdresRepository : IAdresRepository
    {
        public void CreateAdres(Adres adres)
        {
            using (DataContext context = new DataContext())
            {
                context.Adressen.Add(adres);
                context.SaveChanges();
            };
        }
        
        public void DeleteAdres(Guid id)
        {
            using (DataContext context = new DataContext())
            {
                context.Adressen.Remove(GetAdres(id));
                context.SaveChanges();
            };
        }

        public IEnumerable<Adres> GetAdres()
        {
            using (DataContext context = new DataContext())
            {
                var data = context.Adressen.ToList();
                return data;
            }    
        }
        public Adres GetAdres(Guid AdresId)
        {
            return GetAdres().Where(adres => adres.AdresId == AdresId).SingleOrDefault();
        }

        public void UpdateAdres(Adres adres)
        {
            using (DataContext context = new DataContext())
            {
                context.Adressen.Update(adres);
                context.SaveChanges();
            }
        }
    }
}
