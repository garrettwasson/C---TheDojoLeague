using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DojoLeague.Models
{
    public class Ninja : BaseEntity 
    {
        public int ID {get; set;}
        public int? DojoID {get; set;}
        [Required(ErrorMessage = "Name is required")]
        public string NinjaName {get; set;}
        public int NinjaLevel {get; set;}
        public string NinjaDescription {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public Dojo dojos {get; set;}
    }
}