using Newtonsoft.Json;
using SMC.Academico.Common.Areas.DCT.Exceptions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.FilesCollection;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.DCT.Controllers
{
    public class RelatoriosColaboradorController : SMCReportingControllerBase
    {
        #region [ Services ]

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IEntidadeService EntidadeService { get { return this.Create<IEntidadeService>(); } }

        private IColaboradorVinculoService ColaboradorVinculoService { get { return this.Create<IColaboradorVinculoService>(); } }

        private ITipoRelatorioService TipoRelatorioService { get { return this.Create<ITipoRelatorioService>(); } }

        private IInstituicaoModeloRelatorioService InstituicaoModeloRelatorioService { get { return this.Create<IInstituicaoModeloRelatorioService>(); } }

        #endregion [ Services ]

        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("DocenteReport");

        #endregion APIS

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatoriosColaborador";
            }
        }

        public override string ReportBasePath
        {
            get
            {
                return @"\Areas\DCT\Reports\";
            }
        }

        #endregion [ Propriedades ]

        #region [ Actions ]

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var modelo = new RelatorioFiltroViewModel()
            {
                TiposRelatorio = this.TipoRelatorioService.BuscarTiposRelatorioSelect(),
                EntidadesResponsaveis = this.EntidadeService.BuscarGruposProgramasSelect(),
            };

            return View(modelo);
        }

        #endregion [ Actions ]

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult GerarRelatorio(RelatorioFiltroViewModel filtro)
        {

            switch (filtro.TipoRelatorio)
            {
                case Academico.Common.Areas.DCT.Enums.TipoRelatorio.LogAtualizacaoColaborador:
                    return LogAtualizacaoColaborador(filtro);

                case Academico.Common.Areas.DCT.Enums.TipoRelatorio.CertificadoPosDoutor:
                    return CertificadoPosDoutor(filtro);                    
            }

            return null;
            
        }

        [SMCAllowAnonymous]
        public FileContentResult LogAtualizacaoColaborador(RelatorioFiltroViewModel filtro)
        {
            var param = new
            {
                filtro.SelectedValues,
                SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq,
                TipoRelatorio = filtro.TipoRelatorio,
                filtro.DataInicioReferencia,
                filtro.DataFimReferencia,
                filtro.SeqsEntidadesResponsaveis,
                SeqColaborador = filtro.SeqColaborador.Seq,
            };

            var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);

            return new FileContentResult(dadosReport, "application/pdf");
                      

        }

        [SMCAllowAnonymous]
        public FileResult CertificadoPosDoutor(RelatorioFiltroViewModel filtro)
        {
            var dadosCertificado = this.ColaboradorVinculoService.BuscarDadosCertificadoPosDoutor(filtro.Transform<RelatorioCertificadoPosDoutorFiltroData>());
            if (dadosCertificado != null)
            {
                // Recupera o template do relatório
                var seqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq;

                var template = InstituicaoModeloRelatorioService.BuscarTemplateModeloRelatorio(seqInstituicaoEnsino, ModeloRelatorio.CertificadoPosDoc);
                if (template == null)
                    throw new TemplateCertificadoPosDoutorNaoEncontradoException();

                //Tratemento de aspas duplas para que causam impacto no mail merge
                dadosCertificado.NomeColaboradorPosDoutorando = dadosCertificado.NomeColaboradorPosDoutorando;
                dadosCertificado.NomeEntidadeResponsavel = dadosCertificado.NomeEntidadeResponsavel;
                dadosCertificado.NomeProfessorResponsavel = dadosCertificado.NomeProfessorResponsavel;
                dadosCertificado.TituloPesquisa = dadosCertificado.TituloPesquisa;

                string json = JsonConvert.SerializeObject(dadosCertificado);
                var arquivo = SautinSoftHelper.MailMergeToPdf(template.ArquivoModelo.FileData, Guid.NewGuid().ToString(), "dotx", json);

                return File(arquivo, "application/pdf");

            }

            return null;
        }

        #endregion [ Renderizar o Relatório ]

        #region Métodos Auxiliares

        [SMCAllowAnonymous]
        public ActionResult BuscarPosDoutorandosSelect(long? seqEntidadeResponsavel)
        {
            if (seqEntidadeResponsavel.HasValue)
                return Json(ColaboradorVinculoService.BuscarPosDoutorandosSelect(seqEntidadeResponsavel.Value));
            return Json(new List<SMCDatasourceItem>());
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarVinculosColaboradorSelect(long? SeqColaboradorPosDoutorando, long? seqEntidadeResponsavel)
        {
            if (SeqColaboradorPosDoutorando.HasValue && seqEntidadeResponsavel.HasValue)
                return Json(ColaboradorVinculoService.BuscarVinculosColaboradorSelect(SeqColaboradorPosDoutorando.Value, seqEntidadeResponsavel.Value));
            return Json(new List<SMCDatasourceItem>());
        }

        #endregion Métodos Auxiliares

        private string SanatizeMailMerge(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Replace("'", "\"\'")
                        .Replace("\"", "\"\"")
                        .Replace("“", "\"“").Replace("”", "\"”");
        }
    }
}