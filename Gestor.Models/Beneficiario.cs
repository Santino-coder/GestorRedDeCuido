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

        [Required(ErrorMessage = "Por favor, digite el número de identificación.")]
        public int? DNI { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese el nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese los apellidos.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione una fecha de nacimiento.")]
        public DateTime FechaDeNacimiento { get; set; }

        [Range(0, 100, ErrorMessage = "La edad debe estar entre 18 y 100.")]
        public int? Edad { get; set; }


        [Required(ErrorMessage = "Por favor, seleccione el estado civil.")]
        public string EstadoCivil { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione el distrito en el que se encuantra la residencia.")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese la dirección completa.")]
        public string Direccion { get; set; }


        [Required(ErrorMessage = "Por favor, digite el número de teléfono.")]
        public int? Telefono { get; set; }

        [Required(ErrorMessage = "Por favor, digite el ingreso económico mensual y su origen.")]
        public string IngresoEconomicoYOrigen { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione la fecha de ingreso.")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "Por favor, seleccione el estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese el nombre de la persona encargada.")]
        public string PersonaEncargada { get; set; }

        [Required(ErrorMessage = "Por favor, digite el número de teléfono de la persona encargada.")]
        public int NumeroPersonaEncargada { get; set; }

        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "Debe elegir almenos una alternativa")]
        public string Alternativa1 { get; set; }

        public string? Alternativa2 { get; set; }

        public string? Alternativa3 { get; set; }

        public string? Alternativa4 { get; set; }

        public string? Alternativa5 { get; set; }

    }

}
