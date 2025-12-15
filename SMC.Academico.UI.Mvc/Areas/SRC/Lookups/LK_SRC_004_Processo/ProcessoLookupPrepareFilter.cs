using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class ProcessoLookupPrepareFilter : ISMCFilter<ProcessoLookupFiltroViewModel>
    {
        public ProcessoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, ProcessoLookupFiltroViewModel filter)
        {
            var serviceProcessoUnidadeResponsavel = controllerBase.Create<IProcessoUnidadeResponsavelService>();
            var serviceServico = controllerBase.Create<IServicoService>();

            filter.Entidades = serviceProcessoUnidadeResponsavel.BuscarUnidadesResponsaveisVinculadasProcessoSelect();
            filter.Servicos = serviceServico.BuscarServicosPorInstituicaoNivelEnsinoSelect();

            return filter;
        }
    }
}