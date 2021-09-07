using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialBrothersAPIDemo.Models
{
    [Table("Adres")]
    public record Adres
    {   
        [Key]
        [Required]
        public Guid AdresId { get; set; }
        public String Straat { get; set; }
        public String Huisnummer { get; set; }
        public String Postcode { get; set; }
        public String Plaats { get; set; }
        public String Land { get; set; }
    }
}
