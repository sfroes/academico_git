using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class RelatorioConsolidadoServicoCicloLetivoController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ConsolidadoServicoCicloLetivoReport");

        #endregion APIS

        #region [ Services ]

        private IEntidadeService EntidadeService => Create<IEntidadeService>();
        private IServicoService ServicoService => Create<IServicoService>();

        #endregion [ Services ]

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatorioConsolidadoServicoCicloLetivo";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\SRC\Reports\";
            }
        }

        #endregion [ Propriedades ]

        #region [ Actions ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var modelo = new RelatorioConsolidadoServicoCicloLetivoFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect()
            };

            return View(modelo);
        }

        #endregion [ Actions ]

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAllowAnonymous]
        public FileContentResult GerarRelatorio(RelatorioConsolidadoServicoCicloLetivoFiltroViewModel filtro)
        {
            // Se  não informou o filtro de entidades responsáveis, passa como parâmetro todas as entidades
            // que o usuário tem permissão de acesso
            if (filtro.SeqsEntidadeResponsavel == null || filtro.SeqsEntidadeResponsavel.Count == 0)
                filtro.SeqsEntidadeResponsavel = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect().Select(e => e.Seq).ToList();

            var param = new
            {
                SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                filtro.SeqServico,
                filtro.SeqsEntidadeResponsavel
            };
            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            return new FileContentResult(dadosReport, "application/pdf");
        }

        #endregion [ Renderizar o Relatório ]

        #region [ Métodos Auxiliares ]

        [SMCAllowAnonymous]
        public ActionResult BuscarServicosPorCicloLetivoSelect(long? seqCicloLetivo)
        {
            if (seqCicloLetivo.HasValue)
            {
                return Json(ServicoService.BuscarServicosPorCicloLetivoSelect(seqCicloLetivo.Value));
            }

            return Json(string.Empty);
        }

        #endregion [ Métodos Auxiliares ]
    }
}