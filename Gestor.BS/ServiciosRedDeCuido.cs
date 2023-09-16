using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestor.DA;
using Gestor.Models;


namespace Gestor.BS
{
    public class ServiciosRedDeCuido : IServiciosRedDeCuido
    {
        private Gestor.DA.DBContexto ContextoBD;

        public ServiciosRedDeCuido(Gestor.DA.DBContexto contextoBD)
        {
            ContextoBD = contextoBD;
        }


        //Gestiones Beneficiario
        public void AgregarBeneficiario(Beneficiario beneficiario)
        {
            ContextoBD.Beneficiario.Add(beneficiario);
            ContextoBD.SaveChanges();
        }

      
        public List<Beneficiario> ListarBeneficiario()
        {

            List<Beneficiario> listarBeneficiario;
            listarBeneficiario = ContextoBD.Beneficiario.ToList();
            return listarBeneficiario;
        }

 

     

        public void EditarBeneficiario(Beneficiario beneficiario)
        {
            Beneficiario editarBeneficiario;

            editarBeneficiario = ObtenerPorId(beneficiario.idBeneficiario);
            editarBeneficiario.DNI = beneficiario.DNI;
            editarBeneficiario.Nombre = beneficiario.Nombre;
            editarBeneficiario.Apellidos = beneficiario.Apellidos;
            editarBeneficiario.FechaDeNacimiento = beneficiario.FechaDeNacimiento;
            editarBeneficiario.Edad = beneficiario.Edad;
            editarBeneficiario.EstadoCivil = beneficiario.EstadoCivil;
            editarBeneficiario.Distrito = beneficiario.Distrito;
            editarBeneficiario.Direccion = beneficiario.Direccion;
            editarBeneficiario.Telefono = beneficiario.Telefono;
            editarBeneficiario.IngresoEconomicoYOrigen = beneficiario.IngresoEconomicoYOrigen;
            editarBeneficiario.FechaIngreso = beneficiario.FechaIngreso;
            editarBeneficiario.Estado = beneficiario.Estado;
            editarBeneficiario.PersonaEncargada = beneficiario.PersonaEncargada;
            editarBeneficiario.NumeroPersonaEncargada = beneficiario.NumeroPersonaEncargada;
            editarBeneficiario.Observaciones = beneficiario.Observaciones;
            editarBeneficiario.Alternativa1 = beneficiario.Alternativa1;
            editarBeneficiario.Alternativa2 = beneficiario.Alternativa2;
            editarBeneficiario.Alternativa3 = beneficiario.Alternativa3;
            editarBeneficiario.Alternativa4 = beneficiario.Alternativa4;
            editarBeneficiario.Alternativa5 = beneficiario.Alternativa5;

            ContextoBD.Beneficiario.Update(editarBeneficiario);
            ContextoBD.SaveChanges();

        }

        //Gestiones detalle alternativa
        public void AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            ContextoBD.DetalleAlternativa.Add(detalleAlternativa);
            ContextoBD.SaveChanges();
        }

        public List<DetalleAlternativa> ListarDetalleAlternativa()
        {
            List<DetalleAlternativa> listaDetalleAlternativa = ContextoBD.DetalleAlternativa.ToList();
            return listaDetalleAlternativa;
        }

        public List<DetalleAlternativa> ObtenerDetallePorIdBeneficiario(int idBeneficiario)
        {
           var resultado = from c in ContextoBD.DetalleAlternativa
                           where c.idBeneficiario == idBeneficiario
                           select c;

            return resultado.ToList();
        }


        public Beneficiario ObtenerPorId(int id)
        {
            Beneficiario beneficiario;
            beneficiario = ContextoBD.Beneficiario.Find(id);

            return beneficiario;
        }
        public Beneficiario ObtenerBeneficiarioPorId(int id)
        {


            Beneficiario beneficiario;
            beneficiario = ContextoBD.Beneficiario.FirstOrDefault(b => b.idBeneficiario == id);

                return beneficiario;
            
        }
    }
}
