using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using SMC.Framework.UI.Mvc;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Model;
using System.Linq;
using SMC.Academico.ServiceContract.Areas.ORG.Data;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeAtoNormativoLookupPrepareFilter : ISMCFilter<EntidadeAtoNormativoLookupFiltroViewModel>
    {
        public EntidadeAtoNormativoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, EntidadeAtoNormativoLookupFiltroViewModel filter)
        {
            //Inicializa variaveis de configuração
            filter.PermiteAtoNormativo = true;
            filter.LookupEntidadeAtoNormativo = true;

            //Buscar os tipos de Entidade
            var service = controllerBase.Create<ITipoEntidadeService>();
            var item = service.BuscarTiposEntidadeComInstituicao(filter.PermiteAtoNormativo, filter.SeqInstituicaoEnsino);

            if (item != null)
            {
                filter.TiposEntidade = item.Select(s => new SMCDatasourceItem()
                {
                    Seq = s.Seq,
                    Descricao = s.Descricao
                }).ToList();
            }

            var serviceInstituicaoNivel = controllerBase.Create<IInstituicaoNivelService>();
            filter.TiposOrgaoRegulador = serviceInstituicaoNivel.BuscarTipoOrgaoReguladorInstituicao(filter.SeqInstituicaoEnsino);

            return filter;
        }
    }
}
