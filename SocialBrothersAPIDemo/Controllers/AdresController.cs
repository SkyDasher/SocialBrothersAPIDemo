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
using System.Xml;

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
        public string GetKilometresBetweenAdressen(Dictionary<string , Adres> data)
        {
            AdresDto adres1 = data["adres1"].AsDto();
            AdresDto adres2 = data["adres2"].AsDto();

            var beginlatlongurl1 = String.Format("http://dev.virtualearth.net/REST/v1/Locations?countryRegion=" + adres1.Land + "&adminDistrict="+adres1.Plaats+"&locality="+adres1.Plaats+"&postalCode="+adres1.Postcode+"&addressLine="+adres1.Straat+adres1.Huisnummer+"&maxResults=1&o=xml&key="+bingKey);
            WebRequest requestObj1 = WebRequest.Create(beginlatlongurl1);

            HttpWebResponse respobj1 = null;
            respobj1 = (HttpWebResponse)requestObj1.GetResponse();

            string strresult1 = null;
            XmlDocument xmlDoc1 = new XmlDocument();
            using (Stream stream = respobj1.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresult1 = sr.ReadToEnd();
                xmlDoc1.LoadXml(strresult1);
                sr.Close();
            }

            var lat1 = xmlDoc1.GetElementsByTagName("Latitude");
            var long1 = xmlDoc1.GetElementsByTagName("Longitude");

            var latitude1 = "";
            if(lat1 != null)
            {
                foreach(XmlNode curr in lat1)
                {
                    latitude1 = curr.InnerText;
                }
            }

            var longitude1 = "";
            if (long1 != null)
            {
                foreach (XmlNode curr in long1)
                {
                    longitude1 = curr.InnerText;
                }
            }

            string latlong1 = latitude1 +","+ longitude1;

            var beginlatlongurl2 = String.Format("http://dev.virtualearth.net/REST/v1/Locations?countryRegion=" + adres2.Land + "&adminDistrict=" + adres2.Plaats + "&locality=" + adres2.Plaats + "&postalCode=" + adres2.Postcode + "&addressLine=" + adres2.Straat + adres2.Huisnummer + "&maxResults=1&o=xml&key=" + bingKey);
            WebRequest requestObj2 = WebRequest.Create(beginlatlongurl2);

            HttpWebResponse respobj2 = null;
            respobj2 = (HttpWebResponse)requestObj2.GetResponse();

            string strresult2 = null;
            XmlDocument xmlDoc2 = new XmlDocument();
            using (Stream stream = respobj2.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresult2 = sr.ReadToEnd();
                xmlDoc2.LoadXml(strresult2);
                sr.Close();
            }

            var lat2 = xmlDoc2.GetElementsByTagName("Latitude");
            var long2 = xmlDoc2.GetElementsByTagName("Longitude");

            var latitude2 = "";
            if (lat2 != null)
            {
                foreach (XmlNode curr in lat2)
                {
                    latitude2 = curr.InnerText;
                }
            }

            var longitude2 = "";
            if (long2 != null)
            {
                foreach (XmlNode curr in long2)
                {
                    longitude2 = curr.InnerText;
                }
            }

            string latlong2 = latitude2 +","+ longitude2;

            var distanceUrl = String.Format("https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?origins="+latlong1+"&destinations="+latlong2+"&travelMode=driving&o=xml&key=" + bingKey);
            WebRequest distance = WebRequest.Create(distanceUrl);

            HttpWebResponse distanceResp = null;
            distanceResp = (HttpWebResponse)distance.GetResponse();

            string distRes = null;
            XmlDocument x = new XmlDocument();
            using (Stream stream = distanceResp.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                distRes = sr.ReadToEnd();
                x.LoadXml(distRes);
                sr.Close();
            }

            var kilometers = x.GetElementsByTagName("TravelDistance");
            var kmbetweenpoints = "";
            
            if (kilometers != null)
            {
                foreach (XmlNode curr in kilometers)
                {
                    kmbetweenpoints = curr.InnerText;
                }
            }

            return "Kilometers: " + kmbetweenpoints;
        }
    }
}
