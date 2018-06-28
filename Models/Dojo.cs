using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DojoLeague.Models 
{
    public class Dojo : BaseEntity
    {
       public Dojo() 
       {
           ninjas = new List<Ninja>();
       }
       public int ID {get; set;}
       [Required(ErrorMessage = "Name is required")]
       public string DojoName {get; set;}
       [Required(ErrorMessage = "Location is required")]
       public string DojoLocation {get; set;}
       [Required(ErrorMessage = "Additional Dojo informaiton is required")]
       public string DojoDescription {get; set;}
       public DateTime CreatedAt {get; set;}
       public DateTime UpdatedAt {get; set;}
       public ICollection<Ninja> ninjas {get; set;}
    }
}