using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaAtuacaoLookupPrepareFilter : ISMCFilter<PessoaAtuacaoLookupFiltroViewModel>
    {
        public PessoaAtuacaoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, PessoaAtuacaoLookupFiltroViewModel filter)
        {
            filter.TipoAtuacaoSomenteleitura = filter.TipoAtuacao.GetValueOrDefault() == Common.Areas.PES.Enums.TipoAtuacao.Colaborador;

            return filter;
        }
    }
}