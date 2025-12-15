using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLocalidadeLookupPrepareFilter : ISMCFilter<CursoOfertaLocalidadeLookupFiltroViewModel>
    {
        public CursoOfertaLocalidadeLookupFiltroViewModel Filter(SMCControllerBase controllerBase, CursoOfertaLocalidadeLookupFiltroViewModel filter)
        {
            var cursoService = controllerBase.Create<ICursoService>();
            var entidadeService = controllerBase.Create<IEntidadeService>();

            var instituicaoNivelService = controllerBase.Create<IInstituicaoNivelService>();
            filter.NiveisEnsino = instituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();

            filter.Situacoes = cursoService.BuscarSituacoesCursoSelect();

            var grauAcademicoService = controllerBase.Create<IGrauAcademicoService>();
            var tipoFormacaoService = controllerBase.Create<ITipoFormacaoEspecificaService>();

            if (filter.SeqCurso.HasValue)
            {
                filter.NomeCurso = cursoService.BuscarCurso(filter.SeqCurso.Value).Nome;
                filter.TiposFormacaoEspecifica = tipoFormacaoService.BuscarTipoFormacaoEspecificaSelect(new ServiceContract.Areas.CSO.Data.TipoFormacaoEspecificaFiltroData { ClasseTipoFormacao = ClasseTipoFormacao.Curso, Ativo = true });
            }
            else
            {
                filter.TiposFormacaoEspecifica = tipoFormacaoService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(new ServiceContract.Areas.CSO.Data.TipoFormacaoEspecificaPorNivelEnsinoFiltroData { SeqNivelEnsino = (filter.SeqNivelEnsino.HasValue) ? new List<long>() { filter.SeqNivelEnsino.GetValueOrDefault() } : filter.NiveisEnsino.Select(a => a.Seq).ToList(), Ativo = true });
            }

            filter.Localidades = controllerBase.Create<ICursoOfertaLocalidadeService>().BuscarEntidadesSuperioresSelect(false);

            if (filter.ListarDepartamentosGruposProgramas.HasValue && filter.ListarDepartamentosGruposProgramas.Value)
                filter.EntidadesResponsaveis = entidadeService.BuscarDepartamentosGruposProgramasSelect(true);
            else
                filter.EntidadesResponsaveis = cursoService.BuscarHierarquiaSuperiorCursoSelect(false, true, true);

            var instituicaoModalidadeService = controllerBase.Create<IInstituicaoNivelModalidadeService>();
            filter.Modalidades = filter.SeqNivelEnsino.HasValue ?
                instituicaoModalidadeService.BuscarModalidadesPorInstituicaoNivelEnsinoSelect(filter.SeqNivelEnsino.Value) :
                instituicaoModalidadeService.BuscarModalidadesPorInstituicaoSelect();

            filter.SeqCursoIDLeitura = filter.SeqCurso != null && filter.SeqCurso > 0;
            filter.NomeLeitura = !string.IsNullOrEmpty(filter.NomeCurso);
            filter.SeqEntidadeResponsavelLeitura = filter.SeqsEntidadesResponsaveis != null && filter.SeqsEntidadesResponsaveis.Any(a => a != 0);
            filter.SeqNivelEnsinoLeitura = filter.SeqNivelEnsino != null && filter.SeqNivelEnsino > 0;
            filter.SeqsNiveisEnsinoLeitura = filter.SeqsNiveisEnsino != null && filter.SeqsNiveisEnsino.Any(a => a != 0);
            filter.SeqsNiveisEnsinoDisplay = filter.SeqsNiveisEnsino != null && filter.SeqsNiveisEnsino.Any(a => a != 0);
            filter.SeqSituacaoAtualLeitura = filter.SeqSituacaoAtual != null && filter.SeqSituacaoAtual > 0;
            filter.SeqTipoFormacaoEspecificaLeitura = filter.SeqTipoFormacaoEspecifica != null && filter.SeqTipoFormacaoEspecifica > 0;

            return filter;
        }
    }
}