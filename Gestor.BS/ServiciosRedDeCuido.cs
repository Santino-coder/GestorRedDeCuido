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


        //AGREGAR
        public void AgregarBeneficiario(Beneficiario beneficiario)
        {
            ContextoBD.Beneficiario.Add(beneficiario);
            ContextoBD.SaveChanges();
        }
        public void AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            ContextoBD.DetalleAlternativa.Add(detalleAlternativa);
            ContextoBD.SaveChanges();
        }


        //EDITAR
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

        //public void EditarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        //{
        //    DetalleAlternativa detalle;

        //    detalle = ObteneridDetalleAlternativa(detalleAlternativa.idDetalleAlternativa);
        //    detalle.NombreAlternativa = detalleAlternativa.NombreAlternativa;
        //    detalle.Fecha = detalleAlternativa.Fecha;
        //    detalle.Articulo = detalleAlternativa.Articulo;
        //    detalle.Proveedor = detalleAlternativa.Proveedor;
        //    detalle.Cantidad = detalleAlternativa.Cantidad;
        //    detalle.NumeroFactura = detalleAlternativa.NumeroFactura;
        //    detalle.Monto = detalleAlternativa.Monto;
        //    detalle.FacturaFoto = detalleAlternativa.FacturaFoto;
        //    detalle.Proforma = detalleAlternativa.Proforma;




        //    ContextoBD.DetalleAlternativa.Update(detalle);
        //    ContextoBD.SaveChanges();

        //}

        public void EditarDetalleAlternativa(DetalleAlternativa detalleAlternativa)
        {
            try
            {


                ContextoBD.DetalleAlternativa.Update(detalleAlternativa);
                ContextoBD.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }


        //LISTAS

        public List<Beneficiario> ListarBeneficiario()
        {

            List<Beneficiario> listarBeneficiario;
            listarBeneficiario = ContextoBD.Beneficiario.ToList();
            return listarBeneficiario;
        }
        public List<DetalleAlternativa> ListarDetalleAlternativa()
        {
            List<DetalleAlternativa> listaDetalleAlternativa;
            listaDetalleAlternativa = ContextoBD.DetalleAlternativa.ToList();
            return listaDetalleAlternativa;
        }

        public List<DetalleAlternativa> ObtenerDetallePorIdBeneficiario(int idBeneficiario)
        {
           var resultado = from c in ContextoBD.DetalleAlternativa
                           where c.idBeneficiario == idBeneficiario
                           select c;

            return resultado.ToList();
        }

        

        //OBTENER
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

        public DetalleAlternativa ObteneridDetalleAlternativa(int id)
        {
            DetalleAlternativa alternativa;
            alternativa = ContextoBD.DetalleAlternativa.Find(id);

            return alternativa;
        }
    }
}
