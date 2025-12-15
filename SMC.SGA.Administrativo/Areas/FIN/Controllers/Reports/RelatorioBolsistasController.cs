using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.FIN.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{
    public class RelatorioBolsistasController : SMCReportingControllerBase
    {
        #region [ Services ]

        internal IBeneficioService BeneficioService => Create<IBeneficioService>();

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IEntidadeService EntidadeService
        { get { return this.Create<IEntidadeService>(); } }

        private IInstituicaoNivelService InstituicaoNivelService
        { get { return this.Create<IInstituicaoNivelService>(); } }

        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("BolsistasReport");

        #endregion APIS

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatorioBolsistas";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\FIN\Reports\";
            }
        }

        #endregion [ Propriedades ]

        #region [ Actions ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var modelo = new RelatorioBolsistasFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(),
                Beneficios = BeneficioService.BuscarBeneficiosSelect(),
                TiposAtuacoes = new List<SMCDatasourceItem>()
                {
                    new SMCDatasourceItem(){Seq = (long)TipoAtuacao.Aluno, Descricao = SMCEnumHelper.GetDescription(TipoAtuacao.Aluno) },
                    new SMCDatasourceItem(){Seq = (long)TipoAtuacao.Ingressante, Descricao = SMCEnumHelper.GetDescription(TipoAtuacao.Ingressante) },
                },
                NiveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect()
            };

            return View(modelo);
        }

        #endregion [ Actions ]

        #region [ Renderizar o Relatório ]

        private RelatorioBolsistasFiltroViewModel PreencheFiltrosPadrao(RelatorioBolsistasFiltroViewModel filtro)
        {
            // Se o filtro de entidade responsável não foi informado, preenche com todas as entidades
            // que o usuário tem permissão de acesso
            if (filtro.SeqsEntidadesResponsaveis == null || filtro.SeqsEntidadesResponsaveis.Count == 0)
                filtro.SeqsEntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect().Select(s => s.Seq).ToList();

            // Se o filtro de benefício não foi informado, preenche com todos que o usuário tem permissão
            if (filtro.SeqsBeneficios == null || filtro.SeqsBeneficios.Count == 0)
                filtro.SeqsBeneficios = BeneficioService.BuscarBeneficiosSelect().Select(b => b.Seq).ToList();

            return filtro;
        }

        [SMCAllowAnonymous]
        public JsonResult GerarRelatorio(RelatorioBolsistasFiltroViewModel filtro)
        {
            filtro = PreencheFiltrosPadrao(filtro);

            var param = new
            {
                filtro.DataInicioReferencia,
                filtro.DataFimReferencia,
                filtro.TipoAtuacoes,
                filtro.SeqsEntidadesResponsaveis,
                filtro.SeqsBeneficios,
                filtro.SituacaoBeneficio,
                filtro.ExibirParcelasEmAberto,
                filtro.ExibirReferenciaContrato,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq,
                SeqCicloLetivoIngresso = filtro.SeqCicloLetivoIngresso?.Seq,
                SeqNivelEnsino = filtro.SeqNivelEnsino
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
            TempData["Relatorio"] = dadosReport;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        [SMCAllowAnonymous]
        public FileContentResult ApresentarRelatorio()
        {
            return new FileContentResult(TempData["Relatorio"] as byte[], "application/pdf");
        }

        [SMCAllowAnonymous]
        public FileContentResult GerarRelatorioExcel(RelatorioBolsistasFiltroViewModel filtro)
        {
            filtro = PreencheFiltrosPadrao(filtro);

            var param = new
            {
                filtro.DataInicioReferencia,
                filtro.DataFimReferencia,
                filtro.TipoAtuacoes,
                filtro.SeqsEntidadesResponsaveis,
                filtro.SeqsBeneficios,
                filtro.SituacaoBeneficio,
                filtro.ExibirParcelasEmAberto,
                filtro.ExibirReferenciaContrato,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq,
                SeqCicloLetivoIngresso = filtro.SeqCicloLetivoIngresso?.Seq,
                SeqNivelEnsino = filtro.SeqNivelEnsino
            };

            var dadosExcel = ReportAPI.Execute<byte[]>("GerarRelatorioExcel", param, Method.POST);
            return File(dadosExcel, "application/excel", "RelatorioBolsistas.xls");
        }

        #endregion [ Renderizar o Relatório ]

        [SMCAllowAnonymous]
        public ActionResult VerificarAlunoSelecionado(List<long> tipoAtuacoes)
        {
            var alunoSelecionado = tipoAtuacoes.Count == 1 && (int)tipoAtuacoes.First() == (int)TipoAtuacao.Aluno;
            return Json(alunoSelecionado);
        }
    }
}