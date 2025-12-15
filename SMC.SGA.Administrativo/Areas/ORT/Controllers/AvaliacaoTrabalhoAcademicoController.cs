using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using SMC.SGA.Administrativo.Areas.ORT.Models.AvaliacaoTrabalhoAcademico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class AvaliacaoTrabalhoAcademicoController : SMCControllerBase
    {
        #region [ Service ]

        private IApuracaoAvaliacaoService ApuracaoAvaliacaoService => Create<IApuracaoAvaliacaoService>();

        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        private ITrabalhoAcademicoService TrabalhoAcademicoService
        {
            get { return this.Create<ITrabalhoAcademicoService>(); }
        }

        private IAlunoService AlunoService
        {
            get { return this.Create<IAlunoService>(); }
        }

        private IAplicacaoAvaliacaoService AplicacaoAvaliacaoService
        {
            get => Create<IAplicacaoAvaliacaoService>();
        }

        private IInstituicaoExternaService InstituicaoExternaService
        {
            get => Create<IInstituicaoExternaService>();
        }

        private IInstituicaoNivelCalendarioService InstituicaoNivelCalendarioService { get => Create<IInstituicaoNivelCalendarioService>(); }

        #endregion [ Service ]

        // Busca as informações do Trabalho Acadêmico para o cabeçalho
        [SMCAuthorize(UC_ORT_002_02_03.PESQUISAR_AVALIACAO_TRABALHO_ACADEMICO)]
        public ActionResult CabecalhoAvaliacaoTrabalhoAcademico(SMCEncryptedLong seqTrabalhoAcademico)
        {
            AvaliacaoTrabalhoAcademicoCabecalhoViewModel model = ExecuteService<AvaliacaoTrabalhoAcademicoCabecalhoData, AvaliacaoTrabalhoAcademicoCabecalhoViewModel>(TrabalhoAcademicoService.BuscarTrabalhoAcademicoCabecalho, seqTrabalhoAcademico);

            FormatarNomesAutores(model);

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_ORT_002_02_03.PESQUISAR_AVALIACAO_TRABALHO_ACADEMICO)]
        public ActionResult Index(SMCEncryptedLong seqTrabalhoAcademico, DateTime? dataDepositoSecretaria)        
        {
            if (seqTrabalhoAcademico.Value == 0) { return RedirectToAction("Index", "TrabalhoAcademico"); }

            AvaliacaoTrabalhoAcademicoViewModel model = new AvaliacaoTrabalhoAcademicoViewModel();
            model.SeqTrabalhoAcademico = seqTrabalhoAcademico;
            model.DataDepositoSecretaria = dataDepositoSecretaria;

            model.Componentes = ExecuteService<AvaliacaoTrabalhoAcademicoListaData, AvaliacaoTrabalhoAcademicoListaViewModel>
                (AplicacaoAvaliacaoService.BuscarListaComponenteAvaliacoesTrabalhoAcademico, seqTrabalhoAcademico, model.Componentes);

            ValidarAgendamentoBanca(model.Componentes, model.SeqTrabalhoAcademico);
            ValidarLancamentoNota(model.Componentes, seqTrabalhoAcademico);
            return View(model);
        }

        private void ValidarLancamentoNota(List<AvaliacaoTrabalhoAcademicoListaViewModel> componentes, long seqTrabalhoAcademico)
        {
            if (componentes == null || componentes.Count == 0) { return; }

            var seqAluno = TrabalhoAcademicoService.BuscarSeqAlunoTrabalhoAcademico(seqTrabalhoAcademico);

            /* Favor alterar esta consistência, para não permitir excluir a nota lançada se o processo de publicação do trabalho no BDP tiver sido iniciado.
			 * Ou seja, a partir da existência de algum registro na tabela “ort.publicacao_bdp_idioma” para o trabalho em questão.*/
            var registroIdiomaBdpCriado = ValidarPublicacaoBdpTrabalhoAcademico(seqTrabalhoAcademico);

            var situacaoAtualAluno = AlunoService.BuscarSituacaoAtual(seqAluno, true);

            // Se a publicação ou a Situação do Aluno não for Válida para a operação, aborto as validações, com
            // as permissões negadas para lançar e excluir nota
            if (situacaoAtualAluno.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO)
            {

                // Apenas replica as informações dos componentes nas avaliações para funcionamento da tela
                foreach (var componente in componentes)
                {
                    foreach (var avaliacao in componente.Avaliacoes)
                    {
                        avaliacao.JaApurado = !string.IsNullOrEmpty(avaliacao.NotaConceito);
                        avaliacao.PublicacaoBiblioteca = componente.PublicacaoBiblioteca;
                        avaliacao.DataAutorizacaoSegundoDeposito = componente.DataAutorizacaoSegundoDeposito;
                    }
                }

                return;
            }

            foreach (var componente in componentes)
            {
                foreach (var avaliacao in componente.Avaliacoes)
                {
                    avaliacao.PermitirLancarNota = true;
                    avaliacao.JaApurado = !string.IsNullOrEmpty(avaliacao.NotaConceito);
                    avaliacao.PublicacaoBiblioteca = componente.PublicacaoBiblioteca;
                    avaliacao.DataAutorizacaoSegundoDeposito = componente.DataAutorizacaoSegundoDeposito;
                    //Permite ou não lancamento de nota
                    //Este comando estará disponível somente se:
                    // - A apuração não tiver sido apurada, ou seja, com nota ou conceito já lançados;
                    // - E a situação atual da matrícula do aluno for diferente de "Formado";
                    // - E para avaliações cuja a data seja menor ou igual a data atual.
                    // - E para avaliação que Nâo tenha sido cancelada.
                    if (!string.IsNullOrEmpty(avaliacao.NotaConceito) || situacaoAtualAluno.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO || avaliacao.DataInicioAplicacaoAvaliacao > DateTime.Now || avaliacao.DataCancelamento.HasValue)
                        avaliacao.PermitirLancarNota = false;

                    // Permite ou não excluir notas
                    avaliacao.PermitirExclusaoNotaApuracao =
                        !string.IsNullOrEmpty(avaliacao.NotaConceito) && avaliacao.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado
                        && situacaoAtualAluno.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.FORMADO
                        && !registroIdiomaBdpCriado;

                    // Permite ou não excluir avaliação
                    avaliacao.PermitirExlusaoAvaliacao = string.IsNullOrEmpty(avaliacao.NotaConceito);
                }
            }
        }

        /// <summary>
        /// Verifica a existência de algum registro na tabela “ort.publicacao_bdp_idioma” para o trabalho em questão.
        /// </summary>
        /// <param name="seqTrabalhoAcademico"></param>
        /// <returns></returns>
        private bool ValidarPublicacaoBdpTrabalhoAcademico(long seqTrabalhoAcademico)
        {
            bool existePublicacaoIdioma = TrabalhoAcademicoService.VerificaPublicacaoBdpIdioma(seqTrabalhoAcademico);
            return existePublicacaoIdioma;
        }

        [SMCAuthorize(UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA)]
        public ActionResult Incluir(SMCEncryptedLong seqTrabalhoAcademico)
        {
            AvaliacaoTrabalhoAcademicoViewModel model = new AvaliacaoTrabalhoAcademicoViewModel();
            model.SeqTrabalhoAcademico = seqTrabalhoAcademico;
            return View(model);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarInstituicaoExternaColaborador(long? SeqColaborador, bool? ativo)
        {
            if (!SeqColaborador.HasValue) { return Json(null); }

            var result = InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroData() { SeqColaborador = SeqColaborador, Ativo = ativo });

            return Json(result);
        }

        [SMCAuthorize(UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA)]
        public ActionResult VisualizarDetalhesCancelamentoBanca(SMCEncryptedLong seq)
        {
            var filtro = new AvaliacaoTrabalhoAcademicoFiltroData { Seq = seq };
            var model = AplicacaoAvaliacaoService.BuscarDetalhesCancelamentoAplicacaoAvaliacaoBancaExaminadora(filtro).Transform<AvaliacaoTrabalhoAcademicoBancaExaminadoraDynamicModel>();

            return PartialView("_VisualizarDadosCancelamentoBanca", model);
        }

        [SMCAuthorize(UC_ORT_002_02_06.EMITIR_COMPROVANTE_ENTREGA)]
        [SMCAuthorize(UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA)]
        public ActionResult AdicionarAtaDefesa(SMCEncryptedLong seqAplicacaoAvaliacao, SMCEncryptedLong seqTrabalhoAcademico)
        {
            var model = new AlterarAtaDefesaViewModel()
            {
                SeqAplicacaoAvaliacao = seqAplicacaoAvaliacao,
                SeqTrabalhoAcademico = seqTrabalhoAcademico
            };

            return PartialView("AlterarAtaDefesa", model);
        }

        [SMCAuthorize(UC_ORT_002_02_06.EMITIR_COMPROVANTE_ENTREGA)]
        [SMCAuthorize(UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA)]
        public ActionResult AlterarAtaDefesa(SMCEncryptedLong seqArquivoAnexadoAtaDefesa)
        {
            var model = new AlterarAtaDefesaViewModel()
            {
                SeqArquivoAnexadoAtaDefesa = seqArquivoAnexadoAtaDefesa
            };

            return PartialView(model);
        }

        [HttpPost]
        [SMCAuthorize(UC_ORT_002_02_06.EMITIR_COMPROVANTE_ENTREGA)]
        [SMCAuthorize(UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA)]
        public ActionResult SalvarAtaDefesa(AlterarAtaDefesaViewModel model)
        {
            // Carrega o content do arquivo
            SMCUploadHelper.GetFileDataForModel(model);

            // Altera um arquivo anexado
            if (model.SeqArquivoAnexadoAtaDefesa.GetValueOrDefault() > 0)
            {
                // Seta o seq
                model.ArquivoAnexado.GuidFile = model.SeqArquivoAnexadoAtaDefesa.ToString();

                // Salva o novo arquivo anexado
                ArquivoAnexadoService.SalvarArquivo(model.ArquivoAnexado);

                SetSuccessMessage(SMC.SGA.Administrativo.Areas.ORT.Views.AvaliacaoTrabalhoAcademico.App_LocalResources.UIResource.MSG_Sucesso_Alteracao_Ata, "Sucesso", SMCMessagePlaceholders.Centro);
                return Content("OK");
            }
            // Cria um novo arquivo anexado na apuração
            else
            {
                ApuracaoAvaliacaoService.AdicionarArquivoAta(model.SeqAplicacaoAvaliacao.GetValueOrDefault(), model.ArquivoAnexado);

                SetSuccessMessage(SMC.SGA.Administrativo.Areas.ORT.Views.AvaliacaoTrabalhoAcademico.App_LocalResources.UIResource.MSG_Sucesso_Inclusao_Ata, "Sucesso", SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "AvaliacaoTrabalhoAcademico", new { area = "ORT", seqTrabalhoAcademico = new SMCEncryptedLong(model.SeqTrabalhoAcademico.GetValueOrDefault()) });
            }
        }

        #region [ Métodos Privados ]

        private void FormatarNomesAutores(AvaliacaoTrabalhoAcademicoCabecalhoViewModel model)
        {
            if (model is null) { return; }

            model.NomesAutores = new List<string>();
            foreach (var autor in model.Autores)
            {
                string descricaoEnsino = string.IsNullOrEmpty(autor.DescricaoNivelEnsino) ? "" : $" - {autor.DescricaoNivelEnsino}",
                    descricaoCurso = string.IsNullOrEmpty(autor.DescricaoCurso) ? "" : $" - {autor.DescricaoCurso}";

                model.NomesAutores.Add($"{autor.Nome}{descricaoEnsino}{descricaoCurso}");
            }
        }

        private void ValidarAgendamentoBanca(List<AvaliacaoTrabalhoAcademicoListaViewModel> componentes, long seqTrabalhoAcademico)
        {
            foreach (var componente in componentes)
            {
                //chamada que tem como retorno valor true ou false para obrigatoriedade de publicação do trabalho na biblioteca, parametrizada para não gerar financeiro e permitir inclusão manual
                //de acordo com a solicitação no item 2 da task 59395:
                //"2- Para o trabalho cujo componente possui  avaliação cadastrada cuja apuração seja igual a reprovada e o tipo do trabalho esteja parametrizado para não gerar transação financeira
                //na entrega do trabalho, não seja obrigatoria a publicação do trabalho na biblioteca e permite a inclusão manual."
                var bibliotecaObrigatoria = TrabalhoAcademicoService.AtendeRegraHabilitarAgendamentoBanca(seqTrabalhoAcademico);

                var permiteAgendamento = (!componente.Avaliacoes.Any() || componente.Avaliacoes.All(a => a.DataCancelamento.HasValue))
                    || (componente.Avaliacoes.Any(x => x.SituacaoHistoricoEscolar.HasValue && x.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado) && bibliotecaObrigatoria);

                componente.PermitirAgendamentoBanca = permiteAgendamento;
            }
        }

        #endregion [ Métodos Privados ]
    }
}