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
        public ActionResult AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa, IFormFile FacturaFoto, IFormFile Proforma)
        {
            try
            {
                if (FacturaFoto != null && FacturaFoto.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        FacturaFoto.CopyTo(ms);
                        detalleAlternativa.FacturaFoto = ms.ToArray();
                    }
                }

                if (Proforma != null && Proforma.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        Proforma.CopyTo(ms);
                        detalleAlternativa.Proforma = ms.ToArray();
                    }
                }
                ServiciosRedDeCuido.AgregarDetalleAlternativa(detalleAlternativa);
                return RedirectToAction("ListarBeneficiario", "Beneficiario");
            }
            catch
            {
                return View();


            }

        }

        private byte[] ReadFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public ActionResult ListarDetalleAlternativa()
        {
            List<DetalleAlternativa> listar;
            listar = ServiciosRedDeCuido.ListarDetalleAlternativa();
            return View(listar);
        }

        public ActionResult ObtenerDetallePorIdBeneficiario(int idBeneficiario)
        {
            List<DetalleAlternativa> detalles = ServiciosRedDeCuido.ObtenerDetallePorIdBeneficiario(idBeneficiario);

            if (detalles == null)
            {
                TempData["MensajeError"] = "No se encontraron detalles para el beneficiario.";
                // Manejar el caso cuando no se encuentran detalles para el beneficiario
                return RedirectToAction("ListarBeneficiario", "Beneficiario"); // O redirige a donde sea necesario
            }

            return View(detalles);
        }

        public ActionResult SeleccionarBeneficiario(int id)
        {
            TempData["IdBeneficiarioSeleccionado"] = id;
            return RedirectToAction("AgregarDetalleAlternativa");
        }

        public ActionResult MontoTotalPorMes()
        {
            // Obtener todos los detalles de las alternativas
            var detalles = ServiciosRedDeCuido.ListarDetalleAlternativa();

            // Agrupar los detalles por Mes y Año y calcular la suma de montos para cada mes
            var montosPorMes = detalles
                .GroupBy(d => d.Fecha.ToString("yyyy-MM")) // Agrupar por Mes y Año
                .Select(group => new Gestor.UI.Models.MontoPorMesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotal = group.Sum(d => d.Monto)
                })
                .ToList();

            // Puedes pasar los datos a la vista para mostrarlos
            return View(montosPorMes);
        }

    }
}
