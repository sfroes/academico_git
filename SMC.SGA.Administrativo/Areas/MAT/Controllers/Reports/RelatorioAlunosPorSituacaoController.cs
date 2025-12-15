using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.MAT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.MAT.Views.RelatorioAlunosPorSituacao.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using System.Reflection;
using SMC.Framework.Security.Util;

namespace SMC.SGA.Administrativo.Areas.MAT.Controllers
{
    public class RelatorioAlunosPorSituacaoController : SMCReportingControllerBase
    {
        #region APIS

        public SMCApiClient ReportAPI => SMCApiClient.Create("AlunosPorSituacaoReport");

        #endregion APIS

        #region [ Services ]

        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        internal ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();

        private ITurnoService TurnoService { get { return this.Create<ITurnoService>(); } }

        private IEntidadeService EntidadeService { get { return this.Create<IEntidadeService>(); } }

        private ITipoSituacaoMatriculaService TipoSituacaoMatriculaService { get { return this.Create<ITipoSituacaoMatriculaService>(); } }

        private ISituacaoMatriculaService SituacaoMatriculaService { get { return this.Create<ISituacaoMatriculaService>(); } }

        private IAlunoService AlunoService => Create<IAlunoService>();

        #endregion

        #region [ Propriedades ]

        public override string ControllerName
        {
            get
            {
                return "RelatorioAlunosPorSituacao";
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
            var modelo = new RelatorioAlunosPorSituacaoFiltroViewModel()
            {
                EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(),
                TiposSituacaoMatricula = TipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasSelect(),
                TiposVinculoAluno = TipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect(),
                ExibirBotaoGerarArquivo = SMCSecurityHelper.Authorize(UC_MAT_005_05_03.GERAR_ARQUIVO_SMD)
            };

            return View(modelo);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarTurnosCursoOfertaLocalidadeSelect(long? seqCursoOfertaLocalidade)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (seqCursoOfertaLocalidade.HasValue)
            {
                retorno = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade.GetValueOrDefault());
            }
            else
            {
                retorno = this.TurnoService.BuscarTunos();
            }
            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarSituacoesMatricula(List<long> seqsTipoSituacaoMatricula)
        {
            var data = new SituacaoMatriculaFiltroData() { SeqsTipoSituacaoMatricula = seqsTipoSituacaoMatricula };

            return Json(SituacaoMatriculaService.BuscarSituacoesMatricula(data));
        }

        #region [ Renderizar o Relatório ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult GerarRelatorio(RelatorioAlunosPorSituacaoFiltroViewModel filtro)
        {
            // Gerar arquivo txt com os filtros informados
            if (filtro.GerarArquivo)
            {
                return GerarArquivoSMD(filtro);
            }
            else
            {
                var param = new
                {
                    SeqCicloLetivo = filtro.SeqCicloLetivo?.Seq,
                    SeqCicloLetivoIngresso = filtro.SeqCicloLetivoIngresso?.Seq,
                    filtro.SeqsEntidadesResponsaveis,
                    SeqCursoOfertaLocalidade = filtro.SeqCursoOfertaLocalidade?.Seq,
                    filtro.SeqTurno,
                    filtro.SeqsTipoSituacaoMatricula,
                    filtro.SeqsSituacaoMatricula,
                    filtro.SeqsTipoVinculoAluno,
                    filtro.TipoAtuacao,
                    filtro.SituacoesIngressante,
                    filtro.SelectedValues,
                    SeqInstituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado().Seq
                };

                var dadosReport = ReportAPI.Execute<byte[]>("GerarRelatorio", param, Method.POST);
                return new FileContentResult(dadosReport, "application/pdf");
            }
        }

        [SMCAuthorize(UC_MAT_005_05_03.GERAR_ARQUIVO_SMD)]
        public ActionResult GerarArquivoSMD(RelatorioAlunosPorSituacaoFiltroViewModel filtro)
        {
            // Busca a lista de cod-pessoa dos alunos
            var lista = AlunoService.BuscarDadosArquivoSMDAlunosPorSituacao(filtro.Transform<RelatorioAlunosPorSituacaoFiltroData>());

            // Retorno com mensagem de aviso para não gerar arquivo vazio e recarregar a pagina
            if (lista.Count == 0)
            {
                SetAlertMessage(UIResource.MSGArquivoSMDVazio, UIResource.MSGArquivoSMDVazio_Title, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "RelatorioAlunosPorSituacao");
            }

            // Gera um StringBuffer para o conteúdo do arquivo
            StringBuilder ConteudoArquivo = new StringBuilder();

            // Insere uma linha para cada codigo de pessoa no StringBuffer
            foreach (int id in lista)
            {
                ConteudoArquivo.AppendLine(id.ToString());
            }

            // Gera o array de bytes do StringBuffer para retornar os dados do arquivo gerado como um SMCUploadFile ou SMCFile
            byte[] bytes = new byte[ConteudoArquivo.Length * sizeof(char)];
            Buffer.BlockCopy(ConteudoArquivo.ToString().ToCharArray(), 0, bytes, 0, bytes.Length);

            return File(bytes, "text/plain", "dadosSMD.txt");
        }

        #endregion [ Renderizar o Relatório ]

    }
}