using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor.Models
{
    public class Beneficiario
    {
        [Key]
        public int idBeneficiario { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public int DNI { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        [DataType(DataType.Date)]
        public DateTime FechaDeNacimiento { get; set; }
     
        [Range(18, 100, ErrorMessage = "La edad debe estar entre 18 y 100.")]
        public int Edad { get; set; }


        [Required(ErrorMessage = "*Dato requerido.")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "*Dato requerido.")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string IngresoEconomicoYOrigen { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public string PersonaEncargada { get; set; }

        [Required(ErrorMessage = "*Dato requerido.")]
        public int NumeroPersonaEncargada { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "*Debe elegir almenos una alternativa.")]
        public string Alternativa1 { get; set; }

        public string? Alternativa2 { get; set; }

        public string? Alternativa3 { get; set; }

        public string? Alternativa4 { get; set; }

        public string? Alternativa5 { get; set; }

        [NotMapped]
        [JsonProperty("detalleDeAlternativa")]
        public DetalleAlternativa? DetalleAlternativa { get; set; }

        [NotMapped]
        public List<DetalleAlternativa> DetallesAlternativos {get; set; }

        public Beneficiario()
        {
            DetallesAlternativos = new List<DetalleAlternativa>();
        }

    }

}
