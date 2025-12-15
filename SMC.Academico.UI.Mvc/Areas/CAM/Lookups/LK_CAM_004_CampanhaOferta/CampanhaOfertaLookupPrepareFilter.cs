using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CampanhaOfertaLookupPrepareFilter : ISMCFilter<CampanhaOfertaLookupFiltroViewModel>
    {
        public CampanhaOfertaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CampanhaOfertaLookupFiltroViewModel filter)
        {
            var service = controllerBase.Create<IEntidadeService>();
            filter.UnidadesResponsaveis = service.BuscarUnidadesResponsaveisGPILocalSelect();

            // Se campanha tiver valor, bloqueia campanha e ciclo letivo.
            if (filter.SeqCampanha.HasValue)
            {
                filter.SeqCampanhaSomenteLeitura = true;
                filter.CicloLetivoSomenteLeitura = true;
            }
            
            if (!filter.CicloLetivoSomenteLeitura.HasValue)
            {
                filter.CicloLetivoSomenteLeitura = filter.SeqCicloLetivo.HasValue;
            }
            filter.SeqEntidadeResponsavelSomenteLeitura = filter.SeqEntidadeResponsavel.HasValue;
                        
            var campanhaService = controllerBase.Create<ICampanhaService>();
            var filtroCampanha = new CampanhaFiltroData() { SeqEntidadeResponsavel = filter.SeqEntidadeResponsavel };
            filter.Campanhas = campanhaService.BuscarCampanhasSelect(filtroCampanha);

            if (filter.SeqProcessoSeletivo.HasValue)
            {
                PreencherDadosProcessoSeletivo(controllerBase, filter);
            }

            return filter;
        }

        private static void PreencherDadosProcessoSeletivo(SMCControllerBase controllerBase, CampanhaOfertaLookupFiltroViewModel filter)
        {
            filter.SeqProcessoSeletivoSomenteLeitura = true;

            var processoSeletivoService = controllerBase.Create<IProcessoSeletivoService>();
            filter.ProcessosSeletivos = processoSeletivoService.BuscarProcessosSeletivosPorCampanhaSelect(filter.SeqCampanha.GetValueOrDefault());

            var processoSeletivo = processoSeletivoService.BuscarProcessosSeletivo(filter.SeqProcessoSeletivo.Value);
            filter.SeqTipoVinculoAluno = processoSeletivo.SeqTipoVinculoAluno;
            filter.SeqNivelEnsino = processoSeletivo.SeqsNivelEnsino;
        }
    }
}