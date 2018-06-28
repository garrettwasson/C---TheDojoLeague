using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoLeague.Models;
using DojoLeague.Factories;

namespace DojoLeague
{
    public class MainController : Controller 
    {
        [HttpGet("")]
        public IActionResult Main()
        {    
            return View();
        }
        
    }
}
