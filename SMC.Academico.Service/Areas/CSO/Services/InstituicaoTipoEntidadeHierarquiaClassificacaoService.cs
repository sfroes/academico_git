using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Service;
using System.Collections.Generic;
using SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoTipoEntidadeHierarquiaClassificacao;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Framework.Extensions;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class InstituicaoTipoEntidadeHierarquiaClassificacaoService : SMCServiceBase, IInstituicaoTipoEntidadeHierarquiaClassificacaoService
    {
        #region DomainServices

        public InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService
        {
            get { return this.Create<InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService>(); }
        }

        #endregion

        public List<InstituicaoTipoEntidadeHierarquiaClassificacaoData> BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(long SeqInstituicaoTipoEntidade)
        {
            return InstituicaoTipoEntidadeHierarquiaClassificacaoDomainService
                .BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(SeqInstituicaoTipoEntidade)
                .TransformList<InstituicaoTipoEntidadeHierarquiaClassificacaoData>();
        }
    }
}
