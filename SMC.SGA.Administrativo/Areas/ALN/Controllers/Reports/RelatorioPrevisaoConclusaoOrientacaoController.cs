using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Web.Mvc;
using SMC.Framework.UI.Mvc;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc.Security;
using SMC.Academico.Common.Areas.ALN.Constants;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class RelatorioPrevisaoConclusaoOrientacaoController : SMCReportingControllerBase
    {
        #region [ Serviços ]

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();
        private IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();
        private ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();

        #endregion [ Serviços ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("PrevisaoConclusaoOrientacaoReport");

        #endregion

        [SMCAuthorize(UC_ALN_001_11_01.PESQUISAR_PREVISAO_CONCLUSAO_ORIENTACAO_ALUNOS)]
        public ActionResult Index()
        {
            var model = new RelatorioPrevisaoConclusaoOrientacaoFiltroViewModel
            {
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(),
                NiveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect()
            };
            return View(model);
        }

        [SMCAuthorize(UC_ALN_001_11_02.EXIBIR_PREVISAO_CONCLUSAO_ORIENTACAO_ALUNOS)]
        public FileContentResult GerarRelatorio(RelatorioPrevisaoConclusaoOrientacaoFiltroViewModel filtro)
        {
            // Se  não informou o filtro de entidades responsáveis, passa como parâmetro todas as entidades
            // que o usuário tem permissão de acesso
            if (filtro.SeqsEntidadesResponsaveis == null || filtro.SeqsEntidadesResponsaveis.Count == 0)
                filtro.SeqsEntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect().Select(e => e.Seq).ToList();

            var param = new
            {
                filtro.SeqCicloLetivoIngresso.Seq,
                filtro.SeqCicloLetivoIngresso.Descricao,
                filtro.SeqsEntidadesResponsaveis,
                filtro.SeqNivelEnsino,
                filtro.SeqsTipoVinculoAluno,
                filtro.PrazoEncerrado,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        [SMCAuthorize(UC_ALN_001_11_01.PESQUISAR_PREVISAO_CONCLUSAO_ORIENTACAO_ALUNOS)]
        public ActionResult BuscarTipoVinculoAluno(SMCEncryptedLong seqNivelEnsino)
        {
            return Json(TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(seqNivelEnsino));
        }
    }
}