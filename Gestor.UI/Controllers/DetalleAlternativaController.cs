using Gestor.BS;
using Gestor.DA;
using Gestor.Models;
using Gestor.UI.Models;
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

            foreach (var detalle in listar)
            {
                var beneficiario = ServiciosRedDeCuido.ObtenerBeneficiarioPorId(detalle.idBeneficiario);
                detalle.NombreBeneficiario = beneficiario != null ? beneficiario.Nombre : "Beneficiario no encontrado";
                detalle.ApellidoBeneficiario = beneficiario != null ? beneficiario.Apellidos : "Beneficiario no encontrado";
                detalle.NombreCompleto = $"{detalle.NombreBeneficiario} {detalle.ApellidoBeneficiario}";
            
        }

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
        public ActionResult ListarBeneficiario()
        {
            List<Beneficiario> listar;
            listar = ServiciosRedDeCuido.ListarBeneficiario();
            return View(listar);
        }

        public ActionResult MontosTotales()
        {
            var detalles = ServiciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = ServiciosRedDeCuido.ListarBeneficiario();

            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
            };

            return View("MontosTotales", viewModel);
        }


        public ActionResult MontosTotalesPorMes()
        {
            // Obtener todos los detalles de las alternativas
            var detalles = ServiciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = ServiciosRedDeCuido.ListarBeneficiario(); // Reemplaza con el método real

            // Calcular los montos por mes
            var montosPorMes = detalles
                .GroupBy(d => d.Fecha.ToString("yyyy-MM")) // Agrupar por Mes y Año
                .Select(group => new MontosTotalesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotalMes = group.Sum(d => d.Monto)
                })
                .ToList();

            // Calcular los montos por año
            var montosPorAnio = detalles
                .GroupBy(d => d.Fecha.Year) // Agrupar por Año
                .Select(group => new MontosTotalesViewModel
                {
                    Anio = group.Key.ToString(),
                    MontoTotalAnio = group.Sum(d => d.Monto)
                })
                .ToList();

            // Llenar el modelo
            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
                MontoPorMes = montosPorMes,
                MontoPorAnio = montosPorAnio // Agrega esta propiedad al modelo
            };

            return View(viewModel);
        }
        public ActionResult MontosTotalesPorAnio()
        {
            // Obtener todos los detalles de las alternativas
            var detalles = ServiciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = ServiciosRedDeCuido.ListarBeneficiario(); // Reemplaza con el método real

            // Calcular los montos por mes
            var montosPorMes = detalles
                .GroupBy(d => d.Fecha.ToString("yyyy-MM")) // Agrupar por Mes y Año
                .Select(group => new MontosTotalesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotalMes = group.Sum(d => d.Monto)
                })
                .ToList();

            // Calcular los montos por año
            var montosPorAnio = detalles
                .GroupBy(d => d.Fecha.Year) // Agrupar por Año
                .Select(group => new MontosTotalesViewModel
                {
                    Anio = group.Key.ToString(),
                    MontoTotalAnio = group.Sum(d => d.Monto)
                })
                .ToList();

            // Llenar el modelo
            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
                MontoPorMes = montosPorMes,
                MontoPorAnio = montosPorAnio // Agrega esta propiedad al modelo
            };

            return View(viewModel);
        }


       
        public ActionResult MontosTotalesPorBeneficiario()
        {
            var listaBeneficiarios = ServiciosRedDeCuido.ListarBeneficiario();
            var viewModel = new MontosTotalesViewModel
            {
                Beneficiarios = listaBeneficiarios
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult MontosTotalesPorBeneficiario(int idBeneficiario)
        {
            // Obtener detalles solo para el beneficiario seleccionado
            var detalles = ServiciosRedDeCuido.ObtenerDetallePorIdBeneficiario(idBeneficiario);

            // Calcular el monto total para el beneficiario seleccionado
            var montoTotalBeneficiario = detalles.Sum(d => d.Monto);

            // Obtener el beneficiario específico (ajusta esto según cómo obtienes el beneficiario)
            var beneficiario = ServiciosRedDeCuido.ObtenerBeneficiarioPorId(idBeneficiario);

            // Verificar si el beneficiario se encontró correctamente
            if (beneficiario == null)
            {
                // Maneja el caso cuando el beneficiario no se encuentra (por ejemplo, muestra un mensaje de error)
                TempData["MensajeError"] = "El beneficiario no se encontró.";
                return RedirectToAction("ListarBeneficiario", "Beneficiario");
            }
            var montosPorMesBeneficiario = detalles
         .GroupBy(d => d.Fecha.ToString("yyyy-MM")) // Agrupar por Mes y Año
         .Select(group => new MontosTotalesViewModel
         {
            MesBeneficiario = group.Key,
            MontoTotalBeneficiario = group.Sum(d => d.Monto)
         })
         .ToList();

            // Obtener la lista de beneficiarios utilizando ListarBeneficiario
            var listaBeneficiarios = ServiciosRedDeCuido.ListarBeneficiario();

            // Inicializar un nuevo objeto MontosTotalesViewModel
            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,               
                Beneficiario = beneficiario,
                MontoPorBeneficiario = montoTotalBeneficiario,
                Beneficiarios = listaBeneficiarios,
                MontoPorBeneficiarios = montosPorMesBeneficiario

            };

            // Aquí puedes realizar cualquier otro procesamiento necesario

            return View("MontosTotalesPorBeneficiario", viewModel);
        }

        public ActionResult MontosTotalesPorAlternativa()
        {
            var viewModel = new MontosTotalesViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult MontosTotalesPorAlternativa(string AlternativaSeleccionada)
        {
            // Obtener todos los detalles de las alternativas
            var detalles = ServiciosRedDeCuido.ListarDetalleAlternativa();

            // Filtrar los detalles para obtener solo los de la alternativa seleccionada
            var detallesSeleccionados = detalles.Where(d => d.NombreAlternativa == AlternativaSeleccionada).ToList();

            // Calcular el monto total para la alternativa seleccionada
            var montoTotalAlternativa = detallesSeleccionados.Sum(d => d.Monto);
            var montosPorMes = detallesSeleccionados
               .GroupBy(d => d.Fecha.ToString("yyyy-MM"))
               .Select(group => new MontosTotalesViewModel
               {
                   Mesanio = group.Key,
                   MontoTotalMes = group.Sum(d => d.Monto)
               })
               .ToList();
            // Llenar el modelo
            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detallesSeleccionados,
                MontoMensualPorAlternativa = montoTotalAlternativa,
                AlternativaSeleccionada = AlternativaSeleccionada,
                MontoMensualDeAlternativa = montosPorMes
                // Agrega otras propiedades necesarias aquí
            };

            return View("MontosTotalesPorAlternativa", viewModel);
        }



    }
}
