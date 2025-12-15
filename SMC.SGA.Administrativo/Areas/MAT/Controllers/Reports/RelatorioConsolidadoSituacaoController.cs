using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.SGA.Administrativo.Areas.MAT.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.MAT.Controllers
{
    public class RelatorioConsolidadoSituacaoController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ConsolidadoSituacaoReport");

        #endregion APIS

        #region [ Services ]

        private IEntidadeService EntidadeService => Create<IEntidadeService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        #endregion [ Services ]

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatorioConsolidadoSituacao";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\MAT\Reports\";
            }
        }

        #endregion [ Propriedades ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var modelo = new RelatorioConsolidadoSituacaoFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect()
            };

            return View(modelo);
        }

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAllowAnonymous]
        public FileContentResult GerarRelatorio(RelatorioConsolidadoSituacaoFiltroViewModel filtro)
        {
            // Se  não informou o filtro de entidades responsáveis, passa como parâmetro todas as entidades
            // que o usuário tem permissão de acesso
            if (filtro.SeqsEntidadeResponsavel == null || filtro.SeqsEntidadeResponsavel.Count == 0)
                filtro.SeqsEntidadeResponsavel = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect().Select(e => e.Seq).ToList();

            var param = new
            {
                SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                filtro.SeqsEntidadeResponsavel,
                filtro.TipoAtuacoes,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };
            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        #endregion [ Renderizar o Relatório ]
    }
}