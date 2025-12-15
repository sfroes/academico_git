using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLookupPrepareFilter : ISMCFilter<CursoOfertaLookupFiltroViewModel>
    {
        public CursoOfertaLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CursoOfertaLookupFiltroViewModel filter)
        {
            var instituicaoNivelService = controllerBase.Create<IInstituicaoNivelService>();
            filter.NiveisEnsino = instituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();

            var cursoService = controllerBase.Create<ICursoService>();
            filter.EntidadesResponsaveis = cursoService.BuscarHierarquiaSuperiorCursoSelect(filter.EntidadesAtivas, true);
            filter.Situacoes = cursoService.BuscarSituacoesCursoSelect();

            if (!filter.SeqCursoID.HasValue && filter.SeqEntidadeResponsavelFormacao.HasValue)
                filter.SeqCursoID = filter.SeqEntidadeResponsavelFormacao;

            var grauAcademicoService = controllerBase.Create<IGrauAcademicoService>();
            var tipoFormacaoService = controllerBase.Create<ITipoFormacaoEspecificaService>();

            if (filter.SeqCursoID.HasValue)
            {
                filter.Nome = cursoService.BuscarCurso(filter.SeqCursoID.Value).Nome;
                filter.TiposFormacaoEspecifica = tipoFormacaoService.BuscarTipoFormacaoEspecificaSelect(new ServiceContract.Areas.CSO.Data.TipoFormacaoEspecificaFiltroData { ClasseTipoFormacao = ClasseTipoFormacao.Curso, Ativo = true });
            }
            else
            {
                filter.TiposFormacaoEspecifica = tipoFormacaoService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(new ServiceContract.Areas.CSO.Data.TipoFormacaoEspecificaPorNivelEnsinoFiltroData { SeqNivelEnsino = filter.SeqNivelEnsino, Ativo = true });
            }

            if (filter.SeqsGruposProgramasResponsaveis.SMCAny())
            {
                var hierarquiaEntidadeItemService = controllerBase.Create<IHierarquiaEntidadeItemService>();
                filter.SeqsEntidadesResponsaveis = new List<long>();
                foreach (var seqGrupoPrograma in filter.SeqsGruposProgramasResponsaveis)
                {
                    filter.SeqsEntidadesResponsaveis.AddRange(hierarquiaEntidadeItemService.BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(seqGrupoPrograma));
                }
            }

            filter.SeqCursoIDLeitura = filter.SeqCursoID != null && filter.SeqCursoID > 0;
            filter.NomeLeitura = !string.IsNullOrEmpty(filter.Nome);
            filter.SeqEntidadeResponsavelLeitura = filter.SeqsEntidadesResponsaveis != null && filter.SeqsEntidadesResponsaveis.Count > 0;
            filter.SeqNivelEnsinoLeitura = filter.SeqNivelEnsino != null && filter.SeqNivelEnsino.Any();
            filter.SeqSituacaoAtualLeitura = filter.SeqSituacaoAtual != null && filter.SeqSituacaoAtual > 0;
            filter.SeqTipoFormacaoEspecificaLeitura = filter.SeqTipoFormacaoEspecifica != null && filter.SeqTipoFormacaoEspecifica > 0;

            return filter;
        }
    }
}