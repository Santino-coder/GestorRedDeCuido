using Gestor.BS;
using Gestor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.UI.Controllers
{

 
    public class BeneficiarioController : Controller
    {

        private readonly IServiciosRedDeCuido ServiciosRedDeCuido;

        public BeneficiarioController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            ServiciosRedDeCuido = serviciosRedDeCuido;
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

        // Beneficiario listar
        public ActionResult ListarBeneficiario()
        {
            List<Beneficiario> listar;
            listar = ServiciosRedDeCuido.ListarBeneficiario();
            return View(listar);
        }





        // GET: Beneficiarios/Create
        public ActionResult Create()
        {
            return View();
        }

       

        // GET: Beneficiarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Beneficiarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Beneficiarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Beneficiarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
