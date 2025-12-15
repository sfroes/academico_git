using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    class InstituicaoNivelTipoFormacaoEspecificaService : SMCServiceBase, IInstituicaoNivelTipoFormacaoEspecificaService
    {
        #region Domain Service

        private InstituicaoNivelTipoFormacaoEspecificaDomainService InstituicaoNivelTipoFormacaoEspecificaDomainService
        {
            get { return this.Create<InstituicaoNivelTipoFormacaoEspecificaDomainService>(); }
        }

        #endregion Domain Service

        public List<SMCDatasourceItem> BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(long seqInstituicaoNivel)
        {
            return InstituicaoNivelTipoFormacaoEspecificaDomainService.BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(seqInstituicaoNivel);
        }
    }
}
