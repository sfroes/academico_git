using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Aluno.Areas.APR.Models.EntregaOnline;
using SMC.SGA.Aluno.Areas.APR.Views.EntregaOnline.App_LocalResources;
using SMC.SGA.Aluno.Extensions;
using SMC.SGA.Aluno.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.APR.Controllers
{
    public class EntregaOnlineController : SMCControllerBase
    {
        #region Serviços

        private IAlunoHistoricoService AlunoHistoricoService => Create<IAlunoHistoricoService>();

        private IEntregaOnlineService EntregaOnlineService => Create<IEntregaOnlineService>();

        public IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        public IOrigemAvaliacaoService OrigemAvaliacaoService => Create<IOrigemAvaliacaoService>();

        public ITurmaService TurmaService => Create<ITurmaService>();

        #endregion Serviços

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult Index(long seqAplicacaoAvaliacao)
        {
            AlunoSelecionadoViewModel alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            EntregaOnlineViewModel modelo = PreencherModelo(seqAplicacaoAvaliacao);

            return View(modelo);
        }

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult CabecalhoEntregaOnline(long seqAplicacaoAvaliacao)
        {
            CabecalhoEntregaOnlineViewModel2 modelo = new CabecalhoEntregaOnlineViewModel2();
            modelo = EntregaOnlineService.BuscarEntregasOnline(seqAplicacaoAvaliacao).Transform<CabecalhoEntregaOnlineViewModel2>();

            //Nav 11 - Se não existir instruções(texto) e existir o arquivo, exibir uma mensagem padrão,
            //"As instruções estão disponíveis no arquivo anexo." exibir o link de download do arquivo.
            modelo.Instrucao = (modelo.SeqArquivoAnexadoInstrucao.HasValue && string.IsNullOrEmpty(modelo.Instrucao)) ?
                                 UIResource.Instrucao_Padrao : modelo.Instrucao;

            return PartialView("_CabecalhoEntregaOnline2", modelo);
        }

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult SalvarEntregaOnline(EntregaOnlineViewModel modelo)
        {
            try
            {
                if (modelo.Seq > 0 && modelo.ArquivoAnexado != null && modelo.ArquivoAnexado.State == SMCUploadFileState.Changed)
                {
                    Assert(modelo, UIResource.Msg_Assert_Salvar_Entrega_Online);
                }

                EntregaOnlineService.SalvarEntregaOnline(modelo.Transform<EntregaOnlineData>());

                SetSuccessMessage(string.Format(UIResource.Mensagem_Salvar_Entrega_Online), UIResource.Titulo_Sucesso, SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction("Index", routeValues: new { seqAplicacaoAvaliacao = new SMCEncryptedLong(modelo.SeqAplicacaoAvaliacao) });
            }
            catch (Exception ex)
            {
                EntregaOnlineViewModel dadosModelo = PreencherModelo(modelo.SeqAplicacaoAvaliacao);
                dadosModelo.Observacao = modelo.Observacao;
                dadosModelo.ArquivoAnexado = modelo.ArquivoAnexado;
                SetErrorMessage(ex, UIResource.Titulo_Erro, SMCMessagePlaceholders.Centro);
                return View("Index", dadosModelo);
            }
        }

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult SalvarELiberarCorrecao(EntregaOnlineViewModel modelo)
        {
            try
            {
                Assert(modelo, string.Format(UIResource.Msg_Assert_Liberar_Correcao, $"{modelo.Sigla} - {modelo.Descricao}"));

                // Salva e recupera o SEQ
                var seqEntregaOnline = EntregaOnlineService.SalvarEntregaOnline(modelo.Transform<EntregaOnlineData>());

                // Altera a situação para liberado para correção
                EntregaOnlineService.SalvarLiberarCorrecao(seqEntregaOnline);

                SetSuccessMessage(string.Format(UIResource.Mensagem_Salvar_Entrega_Online), UIResource.Titulo_Sucesso, SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction("Index", routeValues: new { seqAplicacaoAvaliacao = new SMCEncryptedLong(modelo.SeqAplicacaoAvaliacao) });
            }
            catch (Exception ex)
            {
                EntregaOnlineViewModel dadosModelo = PreencherModelo(modelo.SeqAplicacaoAvaliacao);
                dadosModelo.Observacao = modelo.Observacao;
                dadosModelo.ArquivoAnexado = modelo.ArquivoAnexado;
                SetErrorMessage(ex, UIResource.Titulo_Erro, SMCMessagePlaceholders.Centro);
                return View("Index", dadosModelo);
            }
        }

        public ActionResult SolicitarLiberarNovaEntrega(long seqEntregaOnline, long seqAplicacaoAvaliacao)
        {
            var model = new SolicitarLiberarNovaEntregaOnlineViewModel { SeqEntregaOnline = seqEntregaOnline, SeqAplicacaoAvaliacao = seqAplicacaoAvaliacao };
            return PartialView("_SolicitarLiberarNovaEntrega", model);
        }

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult SalvarSolicitarLiberarNovaEntrega(SolicitarLiberarNovaEntregaOnlineViewModel modelo)
        {
            EntregaOnlineService.SalvarSolicitarLiberarNovaEntrega(modelo.SeqEntregaOnline, modelo.Observacao);

            SetSuccessMessage(UIResource.Mensagem_Salvar_Solicitar_Liberacao_Nova_Entrega, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", routeValues: new { seqAplicacaoAvaliacao = new SMCEncryptedLong(modelo.SeqAplicacaoAvaliacao) });
        }

        [SMCAuthorize(UC_APR_001_07_01.ENTREGA_ONLINE)]
        public ActionResult LiberarCorrecao(SMCEncryptedLong seqEntregaOnline, SMCEncryptedLong seqAplicacaoAvaliacao)
        {
            AplicacaoAvaliacaoEntregaOnlineData dadosAplicacaoAvaliacao = EntregaOnlineService.BuscarEntregasOnline(seqAplicacaoAvaliacao);

            string descricao = $"{dadosAplicacaoAvaliacao.Sigla} - {dadosAplicacaoAvaliacao.Descricao}";

            Assert(null, string.Format(UIResource.Msg_Assert_Liberar_Correcao, descricao));

            EntregaOnlineService.SalvarLiberarCorrecao(seqEntregaOnline);

            SetSuccessMessage(string.Format(UIResource.Mensagem_Salvar_Entrega_Online), UIResource.Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", routeValues: new { seqAplicacaoAvaliacao = new SMCEncryptedLong(seqAplicacaoAvaliacao) });
        }

        private EntregaOnlineViewModel PreencherModelo(long seqAplicacaoAvaliacao)
        {
            var alunoLogado = this.GetAlunoLogado();
            DateTime dataAtual = DateTime.Now;
            AplicacaoAvaliacaoEntregaOnlineData dadosAplicacaoAvaliacao = EntregaOnlineService.BuscarEntregasOnline(seqAplicacaoAvaliacao);
            EntregaOnlineData dadosEntregaOnlineAluno = dadosAplicacaoAvaliacao.EntregasOnline.FirstOrDefault(f => f.Participantes.Any(a => a.NumeroRA == alunoLogado.Seq));
            EntregaOnlineViewModel modelo = new EntregaOnlineViewModel();
            modelo = dadosAplicacaoAvaliacao.Transform<EntregaOnlineViewModel>();
            modelo.SeqTuma = TurmaService.BuscarSeqTurmaPorOrigemAvaliacao(dadosAplicacaoAvaliacao.SeqOrigemAvaliacao);

            //Nav 11 - Se não existir instruções(texto) e existir o arquivo, exibir uma mensagem padrão,
            //"As instruções estão disponíveis no arquivo anexo." exibir o link de download do arquivo.
            modelo.Instrucao = (modelo.SeqArquivoAnexadoInstrucao.HasValue && string.IsNullOrEmpty(modelo.Instrucao)) ?
                                 UIResource.Instrucao_Padrao : modelo.Instrucao;

            if (dadosEntregaOnlineAluno != null)
            {
                if (dadosEntregaOnlineAluno.SeqArquivoAnexado.HasValue)
                {
                    var arquivoAnexado = ArquivoAnexadoService.BuscarArquivoAnexadoData(dadosEntregaOnlineAluno.SeqArquivoAnexado.Value);
                    modelo.ArquivoAnexado = arquivoAnexado.Transform<SMCUploadFile>();
                    modelo.ArquivoAnexado.GuidFile = arquivoAnexado.UidArquivo.ToString();
                }

                modelo.Observacao = dadosEntregaOnlineAluno.Observacao;
                /*Caso seja a primeira entrega:
                 · Data da entrega = Data atual
                 · Protocolo de entrega = vazio (campo só será preenchido após salvar a primeira entrega)
                 · Situação da entrega = vazio (campo só será preenchido após salvar a primeira entrega)
                 Caso entrega já esteja salva em banco, apresentar as informações já salvas.*/
                modelo.DataEntrega = dadosEntregaOnlineAluno.DataEntrega;
                modelo.SituacaoEntrega = SMCEnumHelper.GetDescription(dadosEntregaOnlineAluno.SituacaoEntrega);
                modelo.CodigoProtocolo = dadosEntregaOnlineAluno.CodigoProtocolo;
                modelo.Seq = dadosEntregaOnlineAluno.Seq;
                modelo.Participantes = dadosEntregaOnlineAluno.Participantes.OrderByDescending(o => o.ResponsavelEntrega).ThenBy(t => t.NomeAluno).TransformMasterDetailList<EntregaOnlineParticipanteViewModel>();
            }
            else
            {
                long seqAlunoHistorico = AlunoHistoricoService.BuscarSequencialAlunoHistoricoAtual(alunoLogado.Seq);
                modelo.Participantes = new SMCMasterDetailList<EntregaOnlineParticipanteViewModel>();
                modelo.Participantes.Add(new EntregaOnlineParticipanteViewModel() { Seq = 0, ResponsavelEntrega = true, SeqAlunoHistorico = seqAlunoHistorico });
            }

            /*Listar os alunos que estão matriculados na turma e/ou divisâo de turma que está ligada à origem de avaliação da
              aplicação de avaliação recebida como parâmetro*/
            modelo.Alunos = OrigemAvaliacaoService.BuscarAlunosPorOrigemAvaliacao(dadosAplicacaoAvaliacao.SeqOrigemAvaliacao);

            //Somente o Aluno respnsável pela entrega poderá alterar os dados e a situação do trabalho
            //Inicio como responsável todo usuario
            bool isResponsavelEntrega = true;

            // Recupera a informação se é ou não a primeira entrega
            bool isPrimeiraEntrega = (dadosEntregaOnlineAluno?.Seq).GetValueOrDefault() == 0;

            //Valido se o participante e se o mesmo logado é o responsavel, caso não exista dados de entrega do aluno assume que é a primeira vez
            //que é a primeira entrega.
            if (dadosEntregaOnlineAluno != null)
                isResponsavelEntrega = dadosEntregaOnlineAluno.Participantes.FirstOrDefault(f => f.NumeroRA == alunoLogado.Seq).ResponsavelEntrega;

            /*“Habilitar os campos:
                “Arquivo para entrega”, “Observação” e “Mestre detalhe de alunos” somente se a data corrente estiver no período da entrega online
                e a situação da entrega seja igual a “Entregue” ou “AguardandoEntrega
                período da entrega = período entre a data de inicio e fim da entrega online definido na aplicação da avaliação.”
                A situação Aguardando entrega é igual a null na primera entrega*/
            modelo.HabilitarCampos = (dataAtual >= dadosAplicacaoAvaliacao.DataInicio && dataAtual <= dadosAplicacaoAvaliacao.DataFim.Value
                                      && (dadosEntregaOnlineAluno == null || dadosEntregaOnlineAluno?.SituacaoEntrega == SituacaoEntregaOnline.Entregue))
                                            && isResponsavelEntrega ? true : false;
            /*Comando habilitado se:
            · data corrente está entre a data de inicio e fim de entrega online definido na aplicação da avaliação.
            · é a primeira entrega ou a situação da entrega é igual a "Entregue".*/
            modelo.HabilitarBotaoNovo = ((dataAtual >= dadosAplicacaoAvaliacao.DataInicio && dataAtual <= dadosAplicacaoAvaliacao.DataFim.Value) &&
                                            (modelo.Seq == 0 || dadosEntregaOnlineAluno?.SituacaoEntrega == SituacaoEntregaOnline.Entregue)) && isResponsavelEntrega ? true : false;
            /*Comando habilitado se:
            · data corrente está entre a data de início e fim da entrega online definido na aplicação da avaliação.
            · a situação da entrega é igual a "Entregue".*/
            modelo.HabilitarBotaoLiberar = ((dataAtual >= dadosAplicacaoAvaliacao.DataInicio && dataAtual <= dadosAplicacaoAvaliacao.DataFim.Value) &&
                                               (dadosEntregaOnlineAluno?.SituacaoEntrega == SituacaoEntregaOnline.Entregue || isPrimeiraEntrega)) && isResponsavelEntrega ? true : false;
            /*Comando habilitado se:
            · data corrente está entre a data de início e fim da entrega online definido na aplicação da avaliação.
            · a situação da entrega é igual a "Liberado para correção".*/
            modelo.HabilitarBotaoNovaEntrega = ((dataAtual >= dadosAplicacaoAvaliacao.DataInicio && dataAtual <= dadosAplicacaoAvaliacao.DataFim.Value) &&
                                                   (dadosEntregaOnlineAluno?.SituacaoEntrega == SituacaoEntregaOnline.LiberadoParaCorrecao)) && isResponsavelEntrega ? true : false;

            //Comando somente dever ser habilitado se já existir uma entrega efetuado
            modelo.HabilitarBotaoHistorico = modelo.Seq > 0;

            return modelo;
        }
    }
}