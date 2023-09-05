using Gestor.BS;
using Gestor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.UI.Controllers
{
    public class AlternativaController : Controller
    {

        private readonly IServiciosRedDeCuido ServiciosRedDeCuido;

        public AlternativaController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            ServiciosRedDeCuido = serviciosRedDeCuido;
        }
        // GET: Alternativa

        public ActionResult AgregarAlternativa(int id)
        {
            Alternativa alternativa = new Alternativa();
            alternativa.idAlternativa = id;

            return View(alternativa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarAlternativa(Alternativa alternativa)
        {
            try
            {
                ServiciosRedDeCuido.AgregarAlternativa(alternativa);
                return RedirectToAction(nameof(ListarAlternativas));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ListarAlternativas()
        {
            List<Alternativa> listar;
            listar = ServiciosRedDeCuido.ListarAlternativas();
            return View(listar);
          
        }

        // GET: Alternativa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alternativa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alternativa/Create
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

        // GET: Alternativa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alternativa/Edit/5
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

        // GET: Alternativa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alternativa/Delete/5
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
