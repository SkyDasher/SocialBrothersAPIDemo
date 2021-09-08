using Microsoft.AspNetCore.Mvc;
using SocialBrothersAPIDemo.DTO;
using SocialBrothersAPIDemo.Models;
using SocialBrothersAPIDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialBrothersAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresController : Controller
    {
        private readonly IAdresRepository repository;
        
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
    }
}
