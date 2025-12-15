using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class PublicacaoBdpReportController : SMCReportingControllerBase
    {
        #region Serviços

        private IPublicacaoBdpService PublicacaoBdpService { get => Create<IPublicacaoBdpService>(); }

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IInstituicaoNivelModeloRelatorioService InstituicaoNivelModeloRelatorioService { get => Create<IInstituicaoNivelModeloRelatorioService>(); }

        private IInstituicaoModeloRelatorioService InstituicaoModeloRelatorioService { get => Create<IInstituicaoModeloRelatorioService>(); }

        private IFuncionarioService FuncionarioService { get => Create<IFuncionarioService>(); }

        #endregion Serviços

        [SMCAllowAnonymous]
        public ActionResult ImprimirAutorizacao(SMCEncryptedLong seq, SMCEncryptedLong seqInstituicaoNivel)
        {
            var dadosPublicacao = this.PublicacaoBdpService.DadosAutorizacaoPublicacaoBdp(seq);
            if (dadosPublicacao != null)
            {
                // Recupera o template do relatório
                var template = InstituicaoNivelModeloRelatorioService.VerificarTemplateModeloRelatorio(dadosPublicacao.NumDiasAutorizacaoParcial, seqInstituicaoNivel);
                if (template == null)
                    throw new TemplatePublicacaoBdpNaoEncontradoException();

                return SMCDocumentMergeInline(string.Format("{0}.pdf", Guid.NewGuid().ToString()), template.ArquivoModelo.FileData, new object[] { dadosPublicacao });
            }

            return null;
        }

        [SMCAuthorize(UC_ORT_003_02_05.EMITIR_FICHA_CATALOGRAFICA)]
        public ActionResult ProcessarFichaCatalografica(FichaCatalograficaViewModel model)
        {
            var instituicao = this.InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();

            // Recupera o template do relatório
            var template = this.InstituicaoModeloRelatorioService.BuscarTemplateModeloRelatorio(instituicao.Seq, modeloRelatorio: ModeloRelatorio.FichaCatalografica);
            if (template == null)
                throw new TemplatePublicacaoBdpNaoEncontradoException();

            var dadosFichaCatalografica = this.PublicacaoBdpService.BuscarDadosImpressaoFichaCatalografica(model.SeqPublicacaoBdp);

            if (dadosFichaCatalografica == null)
                return null;

            if (model.SeqBibliotecario.HasValue)
            {
                var bibliotecario = this.FuncionarioService.BuscarFuncionario(model.SeqBibliotecario.Value);
                dadosFichaCatalografica.NomeBibliotecario = bibliotecario.Nome;
                dadosFichaCatalografica.NumeroCrbBibliotecario = bibliotecario.NumeroRegistroProfissional;
            }

            dadosFichaCatalografica.TituloTrabalho = model.Titulo;
            dadosFichaCatalografica.NumeroCdu = model.CDU;
            dadosFichaCatalografica.Cutter = model.Cutter;
            dadosFichaCatalografica.NomeAluno = model.Aluno;
            dadosFichaCatalografica.PossuiIlustracao = model.PossuiIlustracao.GetValueOrDefault() ? " : il." : " ";

            // Agrupar os assuntos adicionando enumerador na descricao de cada um
            dadosFichaCatalografica.Assuntos = string.Join(", ", model.Assunto.Select((x, index) => (index + 1) + ". " + x.Descricao));

            return SMCDocumentMergeInline(string.Format("{0}.pdf", Guid.NewGuid().ToString()), template.ArquivoModelo.FileData, new object[] { dadosFichaCatalografica });
        }
    }
}