using Gestor.Models;
using Gestor.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GestorDeRed.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Index")]
        public ActionResult<string> Index()
        {
            return Ok("Bienvenido al SI - Punto de inicio (Index).");
        }

        [HttpGet("Privacy")]
        public ActionResult<string> Privacy()
        {
            return Ok("Política de privacidad del SI.");
        }

        [HttpGet("Error")]
        public ActionResult<string> Error()
        {
            return BadRequest("Ha ocurrido un error en el SI.");
        }

    }
}
