using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPIDemo.DTO
{
    public record AdresDto
    {
        public Guid AdresId { get; set; }
        public String Straat { get; set; }
        public String Huisnummer { get; set; }
        public String Postcode { get; set; }
        public String Plaats { get; set; }
        public String Land { get; set; }
    }
}
