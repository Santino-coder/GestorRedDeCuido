using Gestor.BS;
using Gestor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestor.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : Controller
    {
        private readonly IServiciosRedDeCuido ServiciosRedDeCuido;

        public BeneficiarioController(IServiciosRedDeCuido serviciosRedDeCuido)
        {
            ServiciosRedDeCuido = serviciosRedDeCuido;
        }

        [HttpGet("ListarBeneficiario")]
        public IEnumerable<Beneficiario> ListarBeneficiario()
        {
            List<Beneficiario> listarBeneficiario;

            listarBeneficiario = ServiciosRedDeCuido.ListarBeneficiario();

            return listarBeneficiario;
        }

        [HttpGet("ObtenerPorId/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            Beneficiario beneficiario;
            beneficiario = ServiciosRedDeCuido.ObtenerPorId(id);

            
            if (beneficiario == null)
            {
                return NotFound();
            }
            return Ok(beneficiario);
        }

        [HttpPost("AgregarBeneficiario")]
        public IActionResult AgregarBeneficiario([FromBody] Beneficiario beneficiario)
        {
            try
            {
                // Verificar si los datos se vinculan correctamente
                if (ModelState.IsValid)
                {
                    // Agregar lógica para manejar el beneficiario (por ejemplo, guardarlo en una base de datos)
                    ServiciosRedDeCuido.AgregarBeneficiario(beneficiario);

                    // Devolver una respuesta exitosa
                    return Ok(beneficiario);
                }
                else
                {
                    // Devolver errores de validación si el modelo no es válido
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                // Registrar cualquier error que ocurra durante el procesamiento
                Console.WriteLine($"Error al procesar la solicitud: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpPut("EditarBeneficiario/{id}")]
        public IActionResult EditarBeneficiario([FromBody] Beneficiario beneficiario)
        {
            try
            {
                ServiciosRedDeCuido.EditarBeneficiario(beneficiario);
            }
            catch
            {
                return NotFound();
            }

            return Ok(beneficiario);
            
        }

        [HttpGet("CantidadTotalBeneficiarios")]
        public ActionResult<IEnumerable<int>> CantidadTotalBeneficiarios()
        {
            var beneficiarios = ServiciosRedDeCuido.ListarBeneficiario();
            int cantidadTotal = beneficiarios.Count;
            int cantidadActivos = beneficiarios.Count(b => b.Estado == "Activo");
            int cantidadInactivos = beneficiarios.Count(b => b.Estado == "Inactivo");
            int cantidadFallecidos = beneficiarios.Count(b => b.Estado == "Fallecido");

            var result = new List<int>
    {
        cantidadTotal,
        cantidadActivos,
        cantidadInactivos,
        cantidadFallecidos
    };

            return result;
        }

    }
}
