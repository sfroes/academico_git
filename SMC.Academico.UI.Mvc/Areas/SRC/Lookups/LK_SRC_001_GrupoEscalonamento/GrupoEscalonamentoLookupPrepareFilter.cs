using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class GrupoEscalonamentoLookupPrepareFilter : ISMCFilter<GrupoEscalonamentoLookupFiltroViewModel>
    {
        public GrupoEscalonamentoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, GrupoEscalonamentoLookupFiltroViewModel filter)
        {
            var service = controllerBase.Create<IProcessoService>();
            filter.Processos = service.BuscarProcessosSelect(new ProcessoFiltroData());

            filter.SeqProcessoSomenteLeitura = filter.SeqProcesso.HasValue;

            return filter;
        }
    }
}