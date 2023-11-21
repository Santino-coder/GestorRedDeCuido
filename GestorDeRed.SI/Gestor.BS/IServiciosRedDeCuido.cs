using Gestor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestor.BS
{
    public interface IServiciosRedDeCuido
    {

        //Sevicios para beneficiario
        //Agregar

        void AgregarBeneficiario(Beneficiario beneficiario);

        void AgregarDetalleAlternativa(DetalleAlternativa detalleAlternativa);


        //Editar
        void EditarBeneficiario(Beneficiario beneficiario);

        void EditarDetalleAlternativa(DetalleAlternativa detalleAlternativa);


        //Listar
        List<Beneficiario> ListarBeneficiario();

        List<DetalleAlternativa> ListarDetalleAlternativa();

        List<DetalleAlternativa> ObtenerDetallePorIdBeneficiario(int idBeneficiario);


        //Obtener
        Beneficiario ObtenerPorId(int id);

        Beneficiario ObtenerBeneficiarioPorId(int idBeneficiario);

        DetalleAlternativa ObteneridDetalleAlternativa(int id);
    }
}
