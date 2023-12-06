using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.UI.Models.Dtos
{
    public class AgregarAlternativaDto
    {
        [Key]
        public int idDetalleAlternativa { get; set; }

        public string NombreAlternativa { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*Dato requerido.")]
        public DateTime Fecha { get; set; } = DateTime.Now; 

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Articulo { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Proveedor { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public int NumeroFactura { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal Monto { get; set; }
        [JsonIgnore]
        public IFormFile FacturaFoto { get; set; }
        [JsonIgnore]
        public IFormFile Proforma { get; set; }

        public int idBeneficiario { get; set; } // Esta propiedad representa la clave foránea


    }
}