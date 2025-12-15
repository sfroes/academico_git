using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SoliciacaoServicoLookupPrepareFilter : ISMCFilter<SolicitacaoServicoLookupFiltroViewModel>
    {
        public SolicitacaoServicoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, SolicitacaoServicoLookupFiltroViewModel filter)
        {
            var serviceProcesso = controllerBase.Create<IProcessoService>();
            filter.Processos = serviceProcesso.BuscarProcessosSelect(new ProcessoFiltroData());
            filter.SeqProcessoSomenteLeitura = filter.SeqProcesso.HasValue;

            var serviceGrupoEscalonamento = controllerBase.Create<IGrupoEscalonamentoService>();
            filter.GruposEscalonamento = serviceGrupoEscalonamento.BuscarGruposEscalonamentoSelect(new GrupoEscalonamentoFiltroData() { SeqProcesso = filter.SeqProcesso });
            filter.SeqGrupoEscalonamentoSomenteLeitura = filter.SeqGrupoEscalonamento.HasValue;

            var serviceProcessoEtapa = controllerBase.Create<IProcessoEtapaService>();
            filter.ProcessosEtapa = serviceProcessoEtapa.BuscarProcessoEtapaPorProcessoSelect((long)filter.SeqProcesso);
            filter.SeqProcessoEtapaSomenteLeitura = filter.SeqProcessoEtapa.HasValue;

            filter.SituacoesEtapa = serviceProcessoEtapa.BuscarSituacoesEtapaPorProcessoEtapaSelect(1);

            filter.NomeSomenteLeitura = !string.IsNullOrEmpty(filter.Solicitante);

            filter.PassaporteSomenteLeitura = !string.IsNullOrEmpty(filter.Passaporte);

            filter.CPFSomenteLeitura = !string.IsNullOrEmpty(filter.CPF);

            return filter;
        }
    }
}