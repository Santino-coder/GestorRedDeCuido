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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; } 

        public string Articulo { get; set; }

        public string Proveedor { get; set; }

        public string Cantidad { get; set; }

        public int NumeroFactura { get; set; }

        public decimal Monto { get; set; }

        public byte[]? FacturaFoto { get; set; }

        public byte[]? Proforma { get; set; }
        [NotMapped]
        public string NombreBeneficiario { get; set; }

        
        [NotMapped]
        public List<Beneficiario> beneficiario { get; set; }

        [NotMapped]
        public Beneficiario Beneficiario { get; set; }

        [ForeignKey("idBeneficiario")]
        public int idBeneficiario { get; set; } // Esta propiedad representa la clave foránea


    }
}