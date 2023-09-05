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

        public void AgregarBeneficiario(Beneficiario beneficiario)
        {
            ContextoBD.Beneficiario.Add(beneficiario);
            ContextoBD.SaveChanges();
        }

        public List<Beneficiario> ListarBeneficiario() {

            List<Beneficiario> listarBeneficiario;
            listarBeneficiario = ContextoBD.Beneficiario.ToList();
            return listarBeneficiario;
        }
    }
}
