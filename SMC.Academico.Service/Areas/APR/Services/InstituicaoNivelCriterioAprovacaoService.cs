using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class InstituicaoNivelCriterioAprovacaoService : SMCServiceBase, IInstituicaoNivelCriterioAprovacaoService
    {
        #region [ Serviços ]

        private InstituicaoNivelCriterioAprovacaoDomainService InstituicaoNivelCriterioAprovacaoDomainService
        {
            get { return this.Create<InstituicaoNivelCriterioAprovacaoDomainService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> BuscarCriteriosAprovacaoDaInstituicaoNivelSelect(long seqInstituicaoNivel)
        {
            return this.InstituicaoNivelCriterioAprovacaoDomainService.BuscarCriteriosAprovacaoDaInstituicaoNivelSelect(seqInstituicaoNivel);
        }
    }
}
