using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoLookupPrepareFilter : ISMCFilter<CursoLookupFiltroViewModel>
    {
        public CursoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CursoLookupFiltroViewModel filter)
        {
            var instituicaoNivelService = controllerBase.Create<IInstituicaoNivelService>();

            if (filter.ApenasNiveisEnsinoReconhecidosLDB)
                filter.NiveisEnsino = instituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            else
                filter.NiveisEnsino = instituicaoNivelService.BuscarNiveisEnsinoSelect();

            var cursoService = controllerBase.Create<ICursoService>();

            filter.Situacoes = cursoService.BuscarSituacoesCursoSelect();

            filter.EntidadesResponsaveis = cursoService.BuscarHierarquiaSuperiorCursoSelect(filter.ApenasEntidadesComCategoriasAtivas);

            filter.SeqCursoIDLeitura = filter.Seq != null && filter.Seq > 0;
            filter.NomeLeitura = !string.IsNullOrEmpty(filter.Nome);
            filter.SeqEntidadeResponsavelLeitura = filter.SeqsEntidadesResponsaveis != null && filter.SeqsEntidadesResponsaveis.Count > 0;
            filter.SeqNivelEnsinoLeitura = filter.SeqNivelEnsino != null && filter.SeqNivelEnsino.SMCCount() > 0;
            filter.SeqSituacaoAtualLeitura = filter.SeqSituacaoAtual != null && filter.SeqSituacaoAtual > 0;

            return filter;
        }
    }
}