﻿using Gestor.Models;
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

        void AgregarBeneficiario(Beneficiario beneficiario);

        void AgregarAlternativa(Alternativa alternativa);

        void EditarBeneficiario(Beneficiario beneficiario);

        List<Beneficiario> ListarBeneficiario();

        List<Alternativa> ListarAlternativas();

        Beneficiario ObtenerPorId(int id);
    }
}
