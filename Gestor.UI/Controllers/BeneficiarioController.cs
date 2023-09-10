using Gestor.BS;
using Gestor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Gestor.UI.Controllers
{
    public class BeneficiarioController : Controller
    {

        private readonly IServiciosRedDeCuido ServiciosRedDeCuido;

        public BeneficiarioController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            ServiciosRedDeCuido = serviciosRedDeCuido;
        }

        public ActionResult ListarBeneficiario()
        {
            List<Beneficiario> listar;
            listar = ServiciosRedDeCuido.ListarBeneficiario();
            return View(listar);
        }



        //Beneficiarios agregar


        public ActionResult AgregarBeneficiario(int id)
        {
            Beneficiario beneficiario = new Beneficiario();
            beneficiario.idBeneficiario = id;

            return View(beneficiario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarBeneficiario(Beneficiario beneficiario)
        {
            try
            {
                ServiciosRedDeCuido.AgregarBeneficiario(beneficiario);
                return RedirectToAction(nameof(ListarBeneficiario));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeneficiarioController
        public ActionResult Detalles(int id)
        {
            Beneficiario beneficiario;
            beneficiario = ServiciosRedDeCuido.ObtenerPorId(id);
            return View(beneficiario);
        }


        public ActionResult EditarBeneficiario(int id)
        {
            Beneficiario beneficiario;
            beneficiario = ServiciosRedDeCuido.ObtenerPorId(id);

            return View(beneficiario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarBeneficiario(Beneficiario beneficiario)
        {
            try
            {
                ServiciosRedDeCuido.EditarBeneficiario(beneficiario);

                return RedirectToAction(nameof(ListarBeneficiario));
            }
            catch
            {
                return View();
            }
        }

    }
}
