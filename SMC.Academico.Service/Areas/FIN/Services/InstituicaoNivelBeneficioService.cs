using System;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Service;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.FIN.Specifications;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class InstituicaoNivelBeneficioService : SMCServiceBase, IInstituicaoNivelBeneficioService
    {
        #region [ DomainServices ]

        private InstituicaoNivelBeneficioDomainService InstituicaoNivelBeneficioDomainService
        {
            get { return this.Create<InstituicaoNivelBeneficioDomainService>(); }
        }

        #endregion [ DomainServices ]

        public long SalvarInstituicaoNivelBeneficio(InstituicaoNivelBeneficioData instituicaoNivelBeneficio)
        {
            var instituicaoNivelBeneficioVO = instituicaoNivelBeneficio.Transform<InstituicaoNivelBeneficioVO>();
            return InstituicaoNivelBeneficioDomainService.SalvarInstituicaoNivelBeneficio(instituicaoNivelBeneficioVO);
        }

        public SMCPagerData<InstituicaoNivelBeneficioListaData> BuscarInstituicoesNiveisBeneficios(InstituicaoNivelBeneficioFiltroData filtros)
        {
            var spec = filtros.Transform<InstituicaoNivelBeneficioFilterSpecification>();
            return InstituicaoNivelBeneficioDomainService.BuscarInstituicoesNieveisBeneficios(spec).Transform<SMCPagerData<InstituicaoNivelBeneficioListaData>>();            
        }

        public void ExcluirInstituicoesNiveisBeneficios(long seq)
        {
            this.InstituicaoNivelBeneficioDomainService.ExcluirInstituicoesNiveisBeneficios(seq);
        }
    }
}
