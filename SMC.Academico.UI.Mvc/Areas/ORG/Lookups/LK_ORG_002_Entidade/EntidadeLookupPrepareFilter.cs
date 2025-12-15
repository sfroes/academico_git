using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using SMC.Framework.UI.Mvc;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Model;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeLookupPrepareFilter : ISMCFilter<EntidadeLookupFiltroViewModel>
    {
        public EntidadeLookupFiltroViewModel Filter(SMCControllerBase controllerBase, EntidadeLookupFiltroViewModel filter)
        {
            // Busca o item do tipo de hierarquia
            var service = controllerBase.Create<ITipoHierarquiaEntidadeService>();
            var item = service.BuscarTipoHierarquiaEntidadeItem(filter.SeqTipoHierarquiaEntidadeItem);

            if (item != null)
            {
                // Cria a lista de tipos com apenas 1 item
                List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
                lista.Add(new SMCDatasourceItem() { Seq = item.SeqTipoEntidade, Descricao = item.DescricaoTipoEntidade });
                filter.TiposEntidade = lista;

                // Seleciona o filtro de tipo de entidade de acordo com o item do tipo de hierarquia
                filter.SeqTipoEntidade = item.SeqTipoEntidade;
            }

            return filter;
        }
    }
}
