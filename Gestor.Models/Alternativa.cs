using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Models
{
    public class Alternativa
    {

        [Key]
        public int idAlternativa { get; set; }

        public string? Alimentacion { get; set; }

        public string? ArticulosUsoParsonalEHigiene { get; set; }

        public string? MedicamentosEImplementosDeSalud { get; set; }

        public string? AtencionSocialEnSaludIntegral { get; set; }

        public string? ProductosDeApoyoOAyudasTecnicas {  get; set; }   

        public string? EquipamientoDeCasa {  get; set; }

        public string? AlquilerDeViviendaServiciosBasicosMunicipalidadTramiteMigratorio { get; set; }   

        public string? FamiliasSolidarias {  get; set; }

        public string? ServiciosBasicosAtencionEnDomicilioServiciosAtencionYCuidado { get; set; }   

        public string? HogaresComunitarios {  get; set; }

        public string? TransporteYCombustible {  get; set; }

        public string? PromocionYPrevencionDeSalud { get; set; }

        public string? Intitucionalizacion {  get; set; }

        public string? MejorasHabitacionales { get; set;}
    }
}
