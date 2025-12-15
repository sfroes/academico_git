using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.Models;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class BeneficioHistoticoValorAuxilioService : SMCServiceBase, IBeneficioHistoticoValorAuxilioService
    {
        #region [ Services ]

        private BeneficioHistoticoValorAuxilioDomainService BeneficioHistoticoValorAuxilioDomainService
        {
            get { return this.Create<BeneficioHistoticoValorAuxilioDomainService>(); }
        }



        #endregion [ Services ]

        public BeneficioHistoricoValorAuxilioData BuscarDadosValorAuxilio(BeneficioHistoricoValorAuxilioData beneficioHistoricoValorAuxilio)
        {
             return BeneficioHistoticoValorAuxilioDomainService.BuscarDadosValorAuxilio(beneficioHistoricoValorAuxilio.SeqInstituicaoNivelBeneficio).Transform<BeneficioHistoricoValorAuxilioData>();
        }

        public SMCPagerData<BeneficioHistoricoValorAuxilioData> BuscarDadosValoresAuxilio(BeneficioHistoricoValorAuxilioFitroData filtros)
        {
            var spec = filtros.Transform<BeneficioHistoricoValorAuxilioFilterSpecification>();
            return BeneficioHistoticoValorAuxilioDomainService.BuscarDadosValoresAuxilio(spec).Transform<SMCPagerData<BeneficioHistoricoValorAuxilioData>>();
        }

        public long SalvarBeneficioHistoricoValorAuxilio(BeneficioHistoricoValorAuxilioData beneficioHistoricoValorAuxilio)
        {
            var beneficioHistoricoValorAuxilioDados = beneficioHistoricoValorAuxilio.Transform<BeneficioHistoricoValorAuxilio>();

            return BeneficioHistoticoValorAuxilioDomainService.SalvarBeneficioHistoricoValorAuxilio(beneficioHistoricoValorAuxilioDados);
        }
    }
}
