using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AppMvc.Net.Controllers
{
    [Route("he-mat-troi")]
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;
        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }

        [Route("danh-sach-cac-hanh-tinh.html")]
        public IActionResult Index()
        {
            return View();
        }


        [BindProperty(SupportsGet = true, Name = "action")]
        public string Name { get; set; }
        [BindProperty(SupportsGet = true, Name = "id")]
        public int Id { get; set; }

        public IActionResult Mercury()
        {
            var planet = _planetService.Where(planet => planet.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Venus()
        {
            var planet = _planetService.Where(planet => planet.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        //public IActionResult Earth()
        //{
        //    return View();
        //}

        //public IActionResult Mars()
        //{
        //    return View();
        //}

        //public IActionResult Jupiter()
        //{
        //    return View();
        //}

        //public IActionResult Saturn()
        //{
        //    return View();
        //}

        //public IActionResult Uranus()
        //{
        //    return View();
        //}

        //public IActionResult Neptune()
        //{
        //    return View();
        //}

        [Route("hanhtinh/{id:int}")]
        public IActionResult PlanetInfo() 
        {
            var planet = _planetService.Where(planet => planet.Id == Id).FirstOrDefault();
            return View("Detail", planet);
        }

    }
}
