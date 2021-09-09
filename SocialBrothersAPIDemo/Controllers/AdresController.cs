using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SocialBrothersAPIDemo.DTO;
using SocialBrothersAPIDemo.Models;
using SocialBrothersAPIDemo.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace SocialBrothersAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresController : Controller
    {
        private readonly IAdresRepository repository;
        private readonly string bingKey = "AkEEjZUE0nuEgAqbUfL19iQ8vzuEdlo3aKmaTn1-NIQ9KHBvB50PbOAtpLxkHfJV";

        public AdresController(IAdresRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IEnumerable<AdresDto> GetAdres()
        {
            var adressen = repository.GetAdres().Select(adres => adres.AsDto());

            return adressen;
        }
        [HttpGet("{AdresId}")]
        public ActionResult<AdresDto> GetAdres(Guid AdresId)
        {
            var adres = repository.GetAdres(AdresId);

            if (adres is null)
            {
                return NotFound();
            }
            return adres.AsDto();
        }
        [HttpPost]
        public ActionResult<AdresDto> CreateAdres(CreateAdresDto AdresDto)
        {
            Adres adres = new()
            {
                AdresId = Guid.NewGuid(),
                Straat = AdresDto.Straat,
                Huisnummer = AdresDto.Huisnummer,
                Postcode = AdresDto.Postcode,
                Plaats = AdresDto.Plaats,
                Land = AdresDto.Land
            };

            repository.CreateAdres(adres);

            return CreatedAtAction(nameof(GetAdres),new { id = adres.AdresId }, adres.AsDto());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateAdres(Guid id,UpdateAdresDto adresDto)
        {
            var existingAdres = repository.GetAdres(id);
            if(existingAdres is null)
            {
                return NotFound();
            }

            Adres updatedAdres = existingAdres with
            {
                Straat = adresDto.Straat,
                Huisnummer = adresDto.Huisnummer,
                Postcode = adresDto.Postcode,
                Plaats = adresDto.Plaats,
                Land = adresDto.Land
            };

            repository.UpdateAdres(updatedAdres);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteAdres(Guid id)
        {
            var existingAdres = repository.GetAdres(id);
            if (existingAdres is null)
            {
                return NotFound();
            }
            repository.DeleteAdres(id);
            return NoContent();
        }
        [HttpPost]
        [Route("Kilometers")]
        public ActionResult GetKilometresBetweenAdressen(Dictionary<string , Adres> data)
        {
            AdresDto adres1 = data["adres1"].AsDto();
            AdresDto adres2 = data["adres2"].AsDto();

            var beginlatlongurl = String.Format("http://dev.virtualearth.net/REST/v1/Locations?countryRegion=" + adres1.Land + "&adminDistrict="+adres1.Plaats+"&locality="+adres1.Plaats+"&postalCode="+adres1.Postcode+"&addressLine="+adres1.Straat+adres1.Huisnummer+"&maxResults=1&o=xml&key="+bingKey);
            WebRequest requestObj1 = WebRequest.Create(beginlatlongurl);

            HttpWebResponse respobj1 = null;
            respobj1 = (HttpWebResponse)requestObj1.GetResponse();

            string strresult1 = null;
            using (Stream stream = respobj1.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresult1 = sr.ReadToEnd();
                sr.Close();
            }

            return View(strresult1);
        }
    }
}
