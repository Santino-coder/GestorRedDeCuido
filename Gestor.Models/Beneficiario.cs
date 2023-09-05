using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string? Apellido1 { get; set; }

        public string? Apellido2 { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        public int Edad { get; set; }

        public string? EstadoCivil { get; set; }

        public string? Lugar { get; set; }

        public string? Direccion { get; set; }

        public int Telefono { get; set; }

        public string? IngresoEconomicoYOrigen { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string? Estado {  get; set; }

        public string? PersonaEncargada { get; set; }

        public int NumeroPersonaEncargada { get; set; }

        public string? Observaciones { get; set; }

        public int idAlternativa { get; set; }
    }
}
