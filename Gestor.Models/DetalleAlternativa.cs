using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Models
{
    public class DetalleAlternativa
    {
        [Key]
        public int idDetalleAlternativa { get; set; }

        public string NombreAlternativa { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "*Dato requerido.")]
        public DateTime Fecha { get; set; }

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

        public byte[]? FacturaFoto { get; set; }

        public byte[]? Proforma { get; set; }
        [NotMapped]
        public string NombreBeneficiario { get; set; }
        [NotMapped]
        public string ApellidoBeneficiario { get; set; }

        [NotMapped]
        public string NombreCompleto { get; set; }


        [NotMapped]
        public List<Beneficiario> beneficiario { get; set; }

        [NotMapped]
        public Beneficiario Beneficiario { get; set; }

        [ForeignKey("idBeneficiario")]
        public int idBeneficiario { get; set; } // Esta propiedad representa la clave foránea


    }
}