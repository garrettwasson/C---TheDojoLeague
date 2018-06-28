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
    public class DojoController : Controller
    {
        private readonly DojoFactory _dojoFactory;
        public DojoController(DojoFactory dojoFactory)
        {
            _dojoFactory = dojoFactory;
        }
        // GET: /dojos
        [HttpGet("dojos")]
        public IActionResult Main_Dojo()
        {
            ViewBag.allDojos = _dojoFactory.GetAllDojos();
            return View();
        }
        // POST: /dojos/new
        [HttpPost("dojos/new")]
        public IActionResult AddDojo(Dojo dojo)
        {
            if(ModelState.IsValid)
            {
                _dojoFactory.CreateDojo(dojo);
                return RedirectToAction("Main_Dojo");
            }
            ViewBag.allDojos = _dojoFactory.GetAllDojos();
            return View("Main_Dojo");
        }
        // GET: dojos/id
        [HttpGet("dojos/{id}")]
        public IActionResult Dojo(int id)
        {
            ViewBag.dojo = _dojoFactory.GetDojo(id);
            ViewBag.rogueNinjas = _dojoFactory.GetRogueNinjas();
            return View();
        }
        // GET: dojos/id/banish/ninjas/id/update
        [HttpGet("dojos/{dojoID}/banish/ninjas/{ninjaID}/update")]
        public IActionResult DojoBanishment(int dojoID, int ninjaID)
        {
            _dojoFactory.BanishNinja(ninjaID);
            return RedirectToAction("Dojo", new{id = dojoID});
        }
        // GET: dojos/id/recruit/ninjas/id/update
        [HttpGet("dojos/{dojoID}/recruit/ninjas/{ninjaID}/update")]
        public IActionResult DojoRecruitement(int dojoID, int ninjaID)
        {
            _dojoFactory.RecruitNinja(dojoID, ninjaID);
            return RedirectToAction("Dojo", new{id = dojoID});  
        }
    }
}