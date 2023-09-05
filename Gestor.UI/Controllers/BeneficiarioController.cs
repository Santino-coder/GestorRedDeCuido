using Gestor.BS;
using Gestor.Models;
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: BeneficiarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BeneficiarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeneficiarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: BeneficiarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BeneficiarioController/Edit/5
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

        // GET: BeneficiarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BeneficiarioController/Delete/5
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
