using Gestor.BS;
using Gestor.DA;
using Gestor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.UI.Controllers
{
    public class DetalleAlternativaController : Controller
    {

        private readonly IServiciosRedDeCuido ServiciosRedDeCuido;

        public DetalleAlternativaController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            ServiciosRedDeCuido = serviciosRedDeCuido;
        }
        // GET: DetalleAlternativa

        public ActionResult AgregarDetalleAlternativa(int id)
        {
                DetalleAlternativa detalleAlternativa = new DetalleAlternativa();
            detalleAlternativa.idBeneficiario = id;

                return View(detalleAlternativa);
        
        }



        // POST: DetalleAlternativa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {
                ServiciosRedDeCuido.AgregarDetalleAlternativa(detalleAlternativa);
                return RedirectToAction("ListarBeneficiario", "Beneficiario");
            }
            catch
            {
                return View();

              
            }

        }

        public ActionResult SeleccionarBeneficiario(int id)
        {
            TempData["IdBeneficiarioSeleccionado"] = id;
            return RedirectToAction("AgregarDetalleAlternativa");
        }

    }
}
