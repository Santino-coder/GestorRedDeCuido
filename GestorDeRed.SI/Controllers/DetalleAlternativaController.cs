using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gestor.BS;
using Gestor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gestor.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleAlternativaController : Controller
    {
        private readonly IServiciosRedDeCuido _serviciosRedDeCuido;

        public DetalleAlternativaController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            _serviciosRedDeCuido = serviciosRedDeCuido;
        }


        [HttpPost("AgregarDetalleAlternativa")]
        public IActionResult AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Agregar lógica para guardar los datos en la base de datos
                    _serviciosRedDeCuido.AgregarDetalleAlternativa(detalleAlternativa);

                    // Puedes devolver un objeto JSON con detalles de éxito
                    return Ok(new { Message = "Detalle alternativa agregado con éxito" });
                }
                else
                {
                    // Devuelve un código de estado 400 Bad Request con los errores del modelo
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente, puedes logearla o devolver un mensaje de error
                return StatusCode(500, $"Error al agregar detalle alternativa: {ex.Message}");
            }
        }


        [HttpGet("ListarDetalleAlternativa")]
        public ActionResult<IEnumerable<DetalleAlternativa>> ListarDetalleAlternativa()
        {
            var listar = _serviciosRedDeCuido.ListarDetalleAlternativa();

            foreach (var detalle in listar)
            {
                var beneficiario = _serviciosRedDeCuido.ObtenerBeneficiarioPorId(detalle.idBeneficiario);
                detalle.NombreBeneficiario = beneficiario != null ? beneficiario.Nombre : "Beneficiario no encontrado";
                detalle.ApellidoBeneficiario = beneficiario != null ? beneficiario.Apellidos : "Beneficiario no encontrado";
                detalle.NombreCompleto = $"{detalle.NombreBeneficiario} {detalle.ApellidoBeneficiario}";
            }

            return Ok(listar);
        }

        //  ----> Se necesita para editar un detalle 

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<DetalleAlternativa>> ListarDetalleAlternativa(int id)
        {
            var detalle = _serviciosRedDeCuido.ObteneridDetalleAlternativa(id);
            if (detalle != null)
            {
                return Ok(detalle);

            }
            return BadRequest("Detalle no encontrado!");


        }



        [HttpPut("EditarDetalleAlternativa")]
        // [ValidateAntiForgeryToken]
        public IActionResult EditarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Carga el detalle original desde la base de datos
                    // DetalleAlternativa detalleOriginal = _serviciosRedDeCuido.ObteneridDetalleAlternativa(detalleAlternativa.idDetalleAlternativa);


                    _serviciosRedDeCuido.EditarDetalleAlternativa(detalleAlternativa);

                    return Ok("Detalle editado exitosamente!");
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ObtenerDetallePorIdBeneficiario/{idBeneficiario}")]
        public ActionResult<IEnumerable<DetalleAlternativa>> ObtenerDetallePorIdBeneficiario(int idBeneficiario)
        {
            List<DetalleAlternativa> detalles = _serviciosRedDeCuido.ObtenerDetallePorIdBeneficiario(idBeneficiario);

            if (detalles == null)
            {
                // Manejar el caso cuando no se encuentran detalles para el beneficiario
                return NotFound("No se encontraron detalles para el beneficiario.");
            }

            // Obtener el nombre y apellido del beneficiario
            Beneficiario beneficiario = _serviciosRedDeCuido.ObtenerBeneficiarioPorId(idBeneficiario);

            // Completar la propiedad NombreCompleto en cada DetalleAlternativa
            foreach (var detalle in detalles)
            {
                detalle.NombreCompleto = $"{beneficiario.Nombre} {beneficiario.Apellidos}";
            }

            return Ok(detalles);
        }

        [HttpGet("SeleccionarBeneficiario/{id}")]
        public IActionResult SeleccionarBeneficiario(int id)
        {
            // Puedes redirigir o responder con un JSON, dependiendo de tus necesidades
            return Ok(new { IdBeneficiarioSeleccionado = id });
        }


        [HttpGet("ListarBeneficiario")]
        public ActionResult<IEnumerable<Beneficiario>> ListarBeneficiario()
        {
            var listar = _serviciosRedDeCuido.ListarBeneficiario();
            return Ok(listar);
        }

        [HttpGet("MontosTotales")]
        public ActionResult<MontosTotalesViewModel> MontosTotales()
        {
            var detalles = _serviciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = _serviciosRedDeCuido.ListarBeneficiario();

            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
            };

            return Ok(viewModel);
        }

        [HttpGet("MontosTotalesPorMes")]
        public ActionResult<MontosTotalesViewModel> MontosTotalesPorMes()
        {
            var detalles = _serviciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = _serviciosRedDeCuido.ListarBeneficiario();

            var montosPorMes = detalles
                .GroupBy(d => d.Fecha.ToString("yyyy-MM"))
                .Select(group => new MontosTotalesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotalMes = group.Sum(d => d.Monto)
                })
                .ToList();

            var montosPorAnio = detalles
                .GroupBy(d => d.Fecha.Year)
                .Select(group => new MontosTotalesViewModel
                {
                    Anio = group.Key.ToString(),
                    MontoTotalAnio = group.Sum(d => d.Monto)
                })
                .ToList();

            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
                MontoPorMes = montosPorMes,
                MontoPorAnio = montosPorAnio
            };

            return Ok(viewModel);
        }

        [HttpGet("MontosTotalesPorAnio")]
        public ActionResult<MontosTotalesViewModel> MontosTotalesPorAnio()
        {
            var detalles = _serviciosRedDeCuido.ListarDetalleAlternativa();
            var beneficiarios = _serviciosRedDeCuido.ListarBeneficiario();

            var montosPorMes = detalles
                .GroupBy(d => d.Fecha.ToString("yyyy-MM"))
                .Select(group => new MontosTotalesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotalMes = group.Sum(d => d.Monto)
                })
                .ToList();

            var montosPorAnio = detalles
                .GroupBy(d => d.Fecha.Year)
                .Select(group => new MontosTotalesViewModel
                {
                    Anio = group.Key.ToString(),
                    MontoTotalAnio = group.Sum(d => d.Monto)
                })
                .ToList();

            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detalles,
                Beneficiarios = beneficiarios,
                MontoPorMes = montosPorMes,
                MontoPorAnio = montosPorAnio
            };

            return Ok(viewModel);
        }

        [HttpGet("MontosTotalesPorBeneficiario/{id}")]
        public async Task<ActionResult<MontosTotalesViewModel>> MontosTotalesPorBeneficiario(int id)
        {
            try
            {
                var detalles = _serviciosRedDeCuido.ObtenerDetallePorIdBeneficiario(id);
                var montoTotalBeneficiario = detalles.Sum(d => d.Monto);
                var beneficiario = _serviciosRedDeCuido.ObtenerBeneficiarioPorId(id);

                if (beneficiario == null)
                {
                    return NotFound("El beneficiario no se encontró.");
                }

                var montosPorMesBeneficiario = detalles
                    .GroupBy(d => d.Fecha.ToString("yyyy-MM"))
                    .Select(group => new MontosTotalesViewModel
                    {
                        MesBeneficiario = group.Key,
                        MontoTotalBeneficiario = group.Sum(d => d.Monto)
                    })
                    .ToList();

                var listaBeneficiarios = _serviciosRedDeCuido.ListarBeneficiario();
                var viewModel = new MontosTotalesViewModel
                {
                    Detalles = detalles,
                    Beneficiario = beneficiario,
                    MontoPorBeneficiario = montoTotalBeneficiario,
                    Beneficiarios = listaBeneficiarios,
                    MontoPorBeneficiarios = montosPorMesBeneficiario
                };

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                // Maneja el error de acuerdo a tus necesidades (por ejemplo, registrando el error).
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpGet("MontosTotalesPorAlternativa")]
        public ActionResult<MontosTotalesViewModel> MontosTotalesPorAlternativa(string AlternativaSeleccionada)
        {
            var detalles = _serviciosRedDeCuido.ListarDetalleAlternativa();
            var detallesSeleccionados = detalles.Where(d => d.NombreAlternativa == AlternativaSeleccionada).ToList();
            var montoTotalAlternativa = detallesSeleccionados.Sum(d => d.Monto);

            var montosPorMes = detallesSeleccionados
                .GroupBy(d => d.Fecha.ToString("yyyy-MM"))
                .Select(group => new MontosTotalesViewModel
                {
                    Mesanio = group.Key,
                    MontoTotalMes = group.Sum(d => d.Monto)
                })
                .ToList();

            var viewModel = new MontosTotalesViewModel
            {
                Detalles = detallesSeleccionados,
                MontoMensualPorAlternativa = montoTotalAlternativa,
                AlternativaSeleccionada = AlternativaSeleccionada,
                MontoMensualDeAlternativa = montosPorMes
            };

            return Ok(viewModel);
        }
    }
}


