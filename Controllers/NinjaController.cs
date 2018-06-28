using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoLeague.Models;
using DojoLeague.Factories;

namespace DojoLeague.Controllers
{
    public class NinjaController : Controller
    {
        private readonly NinjaFactory _ninjaFactory;
        public NinjaController(NinjaFactory ninjaFactory)
        {
            _ninjaFactory = ninjaFactory;
        }
        // GET: /ninjas
        [HttpGet("ninjas")]
        public IActionResult Main_Ninja()
        {
            ViewBag.allNinjas = _ninjaFactory.GetAllNinjas();
            ViewBag.allDojos = _ninjaFactory.GetAllDojos(); // Will be used for registering ninja to dojo
            return View();
        }
        // POST: /ninjas/new
        [HttpPost("ninjas/new")]
        public IActionResult AddNinja(Ninja ninja)
        {
            if(ModelState.IsValid)
            {
                _ninjaFactory.CreateNinja(ninja);
                return RedirectToAction("Main_Ninja");
            }
            ViewBag.allNinjas = _ninjaFactory.GetAllNinjas();
            ViewBag.allDojos = _ninjaFactory.GetAllDojos(); // Will be used for registering ninja to dojo
            return View("Main_Ninja");
        }
        // GET: /ninja/id
        [HttpGet("ninjas/{id}")]
        public IActionResult Ninja(int id)
        {
            ViewBag.ninja = _ninjaFactory.GetNinja(id);
            return View();
        }
    }
}