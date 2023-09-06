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

        public int DNI { get; set; }

        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        public int Edad { get; set; }

        public string? EstadoCivil { get; set; }

        public string? Distrito { get; set; }

        public string? Direccion { get; set; }

        public int Telefono { get; set; }

        public string? IngresoEconomicoYOrigen { get; set; }

        public DateTime FechaIngreso { get; set; }

        public Boolean Estado {  get; set; }

        public string? PersonaEncargada { get; set; }

        public int NumeroPersonaEncargada { get; set; }

        public string? Observaciones { get; set; }

        public string? Alternativa { get; set; }
    }
}
