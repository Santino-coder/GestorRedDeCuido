using Gestor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.UI.Models
{
    public class MontosTotalesViewModel
    {   
        
      
        public string Mesanio { get; set; }
        public decimal MontoTotalMes { get; set; }
        public List<MontosTotalesViewModel> MontoPorMes { get; set; }

        public string Anio { get; set; }
        public decimal MontoTotalAnio { get; set; }
        public List<MontosTotalesViewModel> MontoPorAnio { get; set; }


        public string MesBeneficiario { get; set; }
        public decimal MontoTotalBeneficiario { get; set; }
        public decimal MontoPorBeneficiario { get; set; } 
        public List<MontosTotalesViewModel> MontoPorBeneficiarios { get; set; }

        public Beneficiario Beneficiario { get; set; }
        public List<DetalleAlternativa>? Detalles { get; set; }
        public List<Beneficiario> Beneficiarios { get; set; }





        public string MesanioAlternativa { get; set; }
        public decimal MontoTotalMesAlternativa { get; set; }
        public string AlternativaSeleccionada { get; set; }
        public decimal MontoMensualPorAlternativa { get; set; }

        public List<MontosTotalesViewModel> MontoMensualDeAlternativa { get; set; }

        public MontosTotalesViewModel()
        {
            Beneficiarios = new List<Beneficiario>();
        }

       

    }
}
