using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.FIN.Models;
using SMC.SGA.Aluno.Areas.FIN.Views.TermoConcessaoBolsa.App_LocalResources;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Controllers
{
    public class TermoConcessaoBolsaController : SMCReportingControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService { get => this.Create<IAlunoService>(); }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();

        private ISMCReportMergeService SMCReportMergeService { get => Create<ISMCReportMergeService>(); }

        private IInstituicaoNivelModeloRelatorioService InstituicaoNivelModeloRelatorioService { get => Create<IInstituicaoNivelModeloRelatorioService>(); }

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService { get => Create<IInstituicaoNivelTipoVinculoAlunoService>(); }

        #endregion [ Services ]

        #region [Constantes]

        private const string GRUPO3 = "Grupo 3";
        private const string GRUPO8 = "Grupo 8";

        #endregion [Constantes]

        [SMCAuthorize(UC_FIN_004_03_01.TERMO_CONCESSAO_BOLSA)]
        public ActionResult Index()
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            var dadosAluno = this.AlunoService.BuscarAluno(alunoLogado.Seq);

            ViewAlunosTermosConcessaoBolsaFiltroData filtro = new ViewAlunosTermosConcessaoBolsaFiltroData() { CodAluno = dadosAluno.CodigoAlunoMigracao, SeqOrigemGra = 1 };

            //Buscar termo na View no GRA
            var termosConcessaoBolsa = this.IntegracaoFinanceiroService.BuscarTermosConcessaoBolsa(filtro);

            TermoConcessaoBolsaViewModel modelo = new TermoConcessaoBolsaViewModel();

            modelo.Beneficios = new List<TermoConcessaoBolsaListaViewModel>();

            modelo.Beneficios = termosConcessaoBolsa.TransformList<TermoConcessaoBolsaListaViewModel>();
            modelo.Beneficios = modelo.Beneficios.OrderByDescending(o => o.DataInicioValidade).ThenByDescending(th => th.Situacao).ToList();

            /* -Caso exista pelo menos um termo de concessão de benefício com a situação de adesão "Pendente", exibir a
            seguinte mensagem de alerta na interface:
            "Existem termos de concessão de bolsa com adesão pendente. Verifique abaixo e clique em "Visualizar
            termo de concessão" para aderir." */
            modelo.ExibirMensagemAlerta = modelo.Beneficios.SMCAny(a => a.EhPendente);

            ViewBag.Title = UIResource.Title_Index;

            return View("Index", modelo);
        }

        [SMCAllowAnonymous]
        public ActionResult VisualizarTermoConcessaoBolsa(long seqTermoConcessaoBolsa)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            //Buscar a Instituição nivel do aluno logado
            long seqInstituicaoNivel = this.InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(alunoLogado.Seq).SeqInstituicaoNivel;

            //Quando se passa o termo de concessão a lista sempre contendo somente o termo exato.
            var dadosTermo = this.IntegracaoFinanceiroService.BuscarDadosEmissaoTermoConcessaoBolsa(new TermoConcessaoBolsaFiltroData() { SeqTermoConcessaoBolsa = (int)seqTermoConcessaoBolsa }).FirstOrDefault();

            //Tratamento dos dados vindo da procedure e ajustando ao modelo dotx
            //Se não foi autenticado remover o texto de adesão
            if (string.IsNullOrEmpty(dadosTermo.CodigoAutenticacaoAdesao))
            {
                dadosTermo.TextoAdesao = string.Empty;
            }
            //Remover os espaços do nome da bolsa
            dadosTermo.DescricaoBolsa = dadosTermo.DescricaoBolsa.Trim();

            /*1.1.Se grupo do termo for igual a 3, exibir o modelo de relatório "Modelo termo de bolsa da reitoria";
            1.2.Se grupo do termo for igual a 8, exibir o modelo de relatório "Modelo termo de bolsa convênio".*/
            ModeloRelatorio modeloRelatorio = dadosTermo.TipoGrupoBolsaEstudo == GRUPO3 ? ModeloRelatorio.ModeloTermoBolsaReitoria : ModeloRelatorio.ModeloTermoBolsaConvênio;

            if (dadosTermo != null)
            {
                // Recupera o template do relatório
                var template = this.InstituicaoNivelModeloRelatorioService.BuscarTemplateModeloRelatorio(seqInstituicaoNivel, modeloRelatorio);
                if (template == null)
                    throw new TemplateTermoConcessaoBolsaNaoEncontradoException();

                return SMCDocumentMergeInline(string.Format("{0}.pdf", Guid.NewGuid().ToString()), template.ArquivoModelo.FileData, new object[] { dadosTermo });
            }

            return null;
        }

        [SMCAllowAnonymous]
        public ActionResult AderirTermoConcessaoBolsa(int seqTermoConcessaoBolsa)
        {
            TermoConcessaoBolsaListaViewModel modelo = new TermoConcessaoBolsaListaViewModel();

            var dadosTermo = this.IntegracaoFinanceiroService.BuscarDadosEmissaoTermoConcessaoBolsa(new TermoConcessaoBolsaFiltroData() { SeqTermoConcessaoBolsa = (int)seqTermoConcessaoBolsa }).FirstOrDefault();

            modelo.SeqTermoConcessaoBolsa = seqTermoConcessaoBolsa;
            modelo.DescricaoTipoLancamento = dadosTermo.DescricaoBolsa.Trim();

            return PartialView("_AderirTermo", modelo);
        }

        [SMCAllowAnonymous]
        public ActionResult ConfirmarAdessaoTermo(int seqTermoConcessaoBolsa)
        {
            this.IntegracaoFinanceiroService.AderirTermoConcessaoBolsa(seqTermoConcessaoBolsa);

            SetSuccessMessage(UIResource.MSG_Adesao_Termo, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Default);

            return SMCRedirectToAction("Index");
        }
    }
}