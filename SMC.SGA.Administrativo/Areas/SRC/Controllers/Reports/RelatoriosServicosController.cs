using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class RelatoriosServicosController : SMCControllerBase
    {
        #region [ Services ]

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IServicoService ServicoService => Create<IServicoService>();

        private ITipoRelatorioServicoService TipoRelatorioServicoService => Create<ITipoRelatorioServicoService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IProcessoService ProcessoService => Create<IProcessoService>();

        private IProcessoEtapaService ProcessoEtapaService => Create<IProcessoEtapaService>();

        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("ServicoReport");

        #endregion APIS

        [SMCAuthorize(UC_SRC_005_03_01.CENTRAL_RELATORIOS_SERVICOS)]
        public ActionResult Index()
        {
            var modelo = new RelatoriosServicosFiltroViewModel()
            {
                TiposRelatorioServico = this.TipoRelatorioServicoService.BuscarTiposRelatorioServicoSelect().TransformList<SMCSelectListItem>(),
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(),
            };

            return View(modelo);
        }

        [SMCAllowAnonymous]
        public FileContentResult ApresentarRelatorio()
        {
            return new FileContentResult(TempData["Relatorio"] as byte[], "application/pdf");
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_005_03_01.CENTRAL_RELATORIOS_SERVICOS)]
        public JsonResult GerarRelatorio(RelatoriosServicosFiltroViewModel filtro)
        {
            // Se  não informou o filtro de entidades responsáveis, passa como parâmetro todas as entidades
            // que o usuário tem permissão de acesso
            if (filtro.SeqsEntidadeResponsavel == null || filtro.SeqsEntidadeResponsavel.Count == 0)
                filtro.SeqsEntidadeResponsavel = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect().Select(e => e.Seq).ToList();

            var param = new
            {
                SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                filtro.SeqServico,
                filtro.SeqsEntidadeResponsavel,
                filtro.SeqsProcessos,
                filtro.SeqProcessoEtapa,
                filtro.TipoRelatorioServico,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            TempData["Relatorio"] = dadosReport;

            return Json("");
        }

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

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessosPorCicloLetivoServicoEntidadesResponsaveisSelect(long? seqCicloLetivo, long? seqServico, List<long> seqsEntidadeResponsavel)
        {
            var retorno = ProcessoService.BuscarProcessosSelect(new ProcessoFiltroData()
            {
                SeqCicloLetivo = seqCicloLetivo,
                SeqServico = seqServico,
                SeqsEntidadesResponsaveis = seqsEntidadeResponsavel != null && seqsEntidadeResponsavel.Any(a => a != 0) ? seqsEntidadeResponsavel.ToArray() : null
            });

            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessoEtapasPorServicoSelect(long? seqServico)
        {
            if (seqServico.HasValue)
            {
                var retorno = ProcessoEtapaService.BuscarProcessoEtapaPorServicoSelect(seqServico);

                return Json(retorno);
            }

            return Json(string.Empty);
        }

        #endregion [ Métodos Auxiliares ]
    }
}