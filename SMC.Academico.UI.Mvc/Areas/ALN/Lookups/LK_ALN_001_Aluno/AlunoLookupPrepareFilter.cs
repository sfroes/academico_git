using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class AlunoLookupPrepareFilter : ISMCFilter<AlunoLookupFiltroViewModel>
    {
        public AlunoLookupFiltroViewModel Filter(SMCControllerBase controllerBase, AlunoLookupFiltroViewModel filter)
        {
            var niveisEnsino = controllerBase.Create<IInstituicaoNivelService>();
            filter.NiveisEnsino = niveisEnsino.BuscarNiveisEnsinoReconhecidoLDBSelect();

            var localidades = controllerBase.Create<ICursoOfertaLocalidadeService>();
            filter.Localidades = localidades.BuscarEntidadesSuperioresSelect(!filter.TelaPesquisa);

            var entidadesResponsaveis = controllerBase.Create<IEntidadeService>();
            filter.EntidadesResponsaveis = entidadesResponsaveis.BuscarUnidadesResponsaveisGPILocalSelect();

            var tipoVinculoAluno = controllerBase.Create<ITipoVinculoAlunoService>();
            filter.TipoVinculoAluno = tipoVinculoAluno.BuscarTiposVinculoAlunoSelect();

            var situacoesMatricula = controllerBase.Create<ISituacaoMatriculaService>();
            filter.SituacoesMatricula = situacoesMatricula.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData());

            filter.SeqNivelEnsinoReadOnly = filter.SeqNivelEnsino.HasValue;
            filter.SeqTipoVinculoAlunoReadOnly = filter.SeqTipoVinculoAluno.HasValue;

            var turnoService = controllerBase.Create<ITurnoService>();

            if (filter.SeqCursoOfertaLocalidade.HasValue)
                filter.Turnos = turnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(filter.SeqCursoOfertaLocalidade.GetValueOrDefault());
            else
                filter.Turnos = turnoService.BuscarTunos();

            return filter;
        }
    }
}