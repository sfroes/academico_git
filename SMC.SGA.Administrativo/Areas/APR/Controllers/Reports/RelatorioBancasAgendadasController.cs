using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Administrativo.Areas.APR.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Controllers
{
    public class RelatorioBancasAgendadasController : SMCReportingControllerBase
    {
        public override string ControllerName => "BancasAgendadas";

        public override string ReportBasePath => @"\Areas\APR\Reports\";

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("BancaReport");

        #endregion

        #region [ Serviços ]

        private IEntidadeService EntidadeService => Create<IEntidadeService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IInstituicaoNivelCalendarioService InstituicaoNivelCalendarioService => Create<IInstituicaoNivelCalendarioService>();
        private IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();

        #endregion [ Serviços ]

        [SMCAuthorize(UC_APR_001_11_01.PESQUISAR_BANCAS_AGENDADAS_PERIODO)]
        public ActionResult Index()
        {
            var modelo = new RelatorioBancasAgendadasFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarGruposProgramasSelect(),
                NiveisEnsino = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect(),
                Ordenacao = OrdenacaoBancasAgendadasRelatorio.DataDefesa,
                TipoMembroBanca = TipoMembroBanca.Orientador
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_APR_001_11_02.EXIBIR_RELATORIO_BANCAS_AGENDADAS_PERIODO)]
        public FileContentResult GerarRelatorio(RelatorioBancasAgendadasFiltroViewModel filtro)
        {
            var param = new
            {
                filtro.SeqEntidadesResponsaveis,
                filtro.SeqNiveisEnsino,
                filtro.SeqTipoEvento,
                filtro.DataInicio,
                filtro.DataFim,
                filtro.SituacaoBanca,
                filtro.Ordenacao,
                filtro.ExibirBancasComNota,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq,
                SeqColaborador = filtro.SeqColaborador?.Seq,
                filtro.TipoMembroBanca
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        [SMCAuthorize(UC_APR_001_11_01.PESQUISAR_BANCAS_AGENDADAS_PERIODO)]
        public ActionResult BuscarTipoEventoNivelEnsino(List<long> seqNiveisEnsino)
        {
            if (!seqNiveisEnsino.SMCAny()) { return Json(null); }

            var result = InstituicaoNivelCalendarioService.BuscarTiposEventosCalendario(seqNiveisEnsino);

            return Json(result);
        }
    }
}