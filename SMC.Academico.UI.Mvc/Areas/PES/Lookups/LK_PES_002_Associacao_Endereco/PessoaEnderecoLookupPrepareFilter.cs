using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoLookupPrepareFilter : ISMCFilter<PessoaEnderecoLookupFiltroViewModel>
    {
        public PessoaEnderecoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, PessoaEnderecoLookupFiltroViewModel filter)
        {
            filter.EnderecosCorrespondencia = controllerBase.Create<IPessoaEnderecoService>().BuscarEnderecosCorrespondenciaSelect(filter.TipoAtuacao);

            return filter;
        }
    }
}