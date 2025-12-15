using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.SGA.Administrativo.App_GlobalResources;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class GrupoEscalonamentoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private IGrupoEscalonamentoService GrupoEscalonamentoService { get => Create<IGrupoEscalonamentoService>(); }

        private IProcessoService ProcessoService { get => Create<IProcessoService>(); }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO)]
        public ActionResult CabecalhoProcesso(SMCEncryptedLong seqProcesso)
        {
            return new ProcessoController().CabecalhoProcesso(seqProcesso, false);
        }

        [SMCAuthorize(UC_SRC_002_06_02.MANTER_GRUPO_ESCALONAMENTO_PROCESSO)]
        public ActionResult CabecalhoCompleto(long seqProcesso, long seq)
        {
            GrupoEscalonamentoCabecalhoViewModel modeloCabecalho = new GrupoEscalonamentoCabecalhoViewModel();

            //Se for edição do grupo de escalonamento 
            if (seq != 0)
            {
                modeloCabecalho = GrupoEscalonamentoService.BuscarCabecalhoGrupoEscalonamento(seq).Transform<GrupoEscalonamentoCabecalhoViewModel>();
            }
            else
            {
                var modeloProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<ProcessoCabecalhoViewModel>();
                modeloCabecalho = modeloProcesso.Transform<GrupoEscalonamentoCabecalhoViewModel>();
            }

            return PartialView("_CabecalhoCompleto", modeloCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_06_04.COPIAR_GRUPO_ESCALONAMENTO)]
        public ActionResult CopiarGrupoEscalonamento(SMCEncryptedLong seqGrupoEscalonamento)
        {
            try
            {
                GrupoEscalonamentoData data = GrupoEscalonamentoService.BuscarGrupoEscalonamento(seqGrupoEscalonamento);

                GrupoEscalonamentoCopiarViewModel modelo = new GrupoEscalonamentoCopiarViewModel();
                modelo.DescricaoGrupoEscalonamento = data.Descricao;
                modelo.Itens = data.Itens.TransformList<GrupoEscalonamentoCopiarListarItemViewModel>();
                modelo.NumeroDivisaoParcelas = data.NumeroDivisaoParcelas;
                modelo.SeqGrupoEscalonamento = seqGrupoEscalonamento;
                modelo.Mensagem = UIResource.Copia_Informacao_Mensagem;
                return PartialView("_CopiarGrupoEscalonamento", modelo);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        [SMCAuthorize(UC_SRC_002_06_02.MANTER_GRUPO_ESCALONAMENTO_PROCESSO)]
        public ActionResult ValidarGrupoEscalonamento(SMCEncryptedLong seqProcesso, SMCEncryptedLong seqGrupoEscalonamento)
        {
            var retorno = this.GrupoEscalonamentoService.ValidarGrupoEscalonamento(seqGrupoEscalonamento);

            SetSuccessMessage(Views.GrupoEscalonamento.App_LocalResources.UIResource.Mensagem_Sucesso_Validar_Grupo, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "GrupoEscalonamento", routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [SMCAuthorize(UC_SRC_002_06_02.MANTER_GRUPO_ESCALONAMENTO_PROCESSO)]
        public ActionResult DesativarGrupoEscalonamento(SMCEncryptedLong seqProcesso, SMCEncryptedLong seqGrupoEscalonamento)
        {
            var retorno = this.GrupoEscalonamentoService.DesativarGrupoEscalonamento(seqGrupoEscalonamento);

            SetSuccessMessage(Views.GrupoEscalonamento.App_LocalResources.UIResource.Mensagem_Sucesso_Desativar_Grupo, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "GrupoEscalonamento", routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_002_06_04.COPIAR_GRUPO_ESCALONAMENTO)]
        public ActionResult CopiarGrupoEscalonamento(GrupoEscalonamentoCopiarViewModel modelo)
        {
            try
            {
                GrupoEscalonamentoService.CopiarGrupoEscalonamento(modelo.SeqGrupoEscalonamento, modelo.Descricao);
                SetSuccessMessage("Cópia do Grupo de escalonamento efetuada com sucesso!", target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }
            return SMCRedirectToUrl(Request.UrlReferrer.ToString());
        }

        [SMCAuthorize(UC_SRC_002_06_05.ASSOCIAR_SOLICITACAO_SERVICO)]
        public ActionResult IncluirAssociarSolicitacaoGrupoEscalonamento(SMCEncryptedLong seqGrupoEscalonamento, SMCEncryptedLong seqProcesso)
        {
            try
            {
                ///Verifica se o grupo de escalonamento tem alguma restrição
                GrupoEscalonamentoService.BuscarGrupoEscalonamento(seqGrupoEscalonamento);

                GrupoEscalonamentoAssociarSolicitacaoViewModel modelo = new GrupoEscalonamentoAssociarSolicitacaoViewModel();

                modelo.SeqGrupoEscalonamento = seqGrupoEscalonamento;
                modelo.SeqProcesso = seqProcesso;

                return PartialView("_AssociarSolicitacaoGrupoEscalonamento", modelo);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }

        }

        [SMCAuthorize(UC_SRC_002_06_05.ASSOCIAR_SOLICITACAO_SERVICO)]
        public ActionResult EnviarNotificacaoPrazoVigencia(SMCEncryptedLong seqGrupoEscalonamento, SMCEncryptedLong seqProcesso)
        {
            try
            {
                this.GrupoEscalonamentoService.EnviarNotificacaoPrazoVigencia(seqGrupoEscalonamento);

                SetSuccessMessage(UIResource.Mensagem_Enviar_Notificacao_Grupo_Escalonamento, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                this.ThrowRedirect(ex, null);
            }

            return SMCRedirectToUrl(Request.UrlReferrer.ToString());

        }

        [SMCAuthorize(UC_SRC_002_06_05.ASSOCIAR_SOLICITACAO_SERVICO)]
        public ActionResult SalvarAssociarSolicitacaoGrupoEscalonamento(GrupoEscalonamentoAssociarSolicitacaoViewModel modeloView)
        {
            try
            {
                GrupoEscalonamentoData modelo = new GrupoEscalonamentoData();

                modelo.Seq = modeloView.SeqGrupoEscalonamento;
                modelo.SeqProcesso = modeloView.SeqProcesso;
                modelo.SeqSolicitacaoServico = modeloView.SeqSolicitacaoServico.Seq;

                this.GrupoEscalonamentoService.ValidarAssociacaoSolicitacao(modelo);

                ///Cenário que a etapa atual da solicitação foi encerrada - Disciplina isolada
                ///Se a etapa atual da solicitação foi encerrada, o processo refere - se a solicitação de matrícula e de
                ///disciplina isolada, deverá verificar se há disponível vaga na respectiva turma, para os itens da
                ///solicitação que estão cancelados e o motivo igual a etapa finalizada.
                if (this.GrupoEscalonamentoService.ValidarAssertAssociacaoSolicitacaoGrupoEscalonamento(modelo.SeqSolicitacaoServico))
                {
                    var retornoValidacaoVagas = this.GrupoEscalonamentoService.ValidacaoQuantidadeVagasPelaSolicitacao(modelo.SeqSolicitacaoServico);

                    ///Se algumas turmas tiverem vagas exibe mensagem de confirmação
                    Assert(modeloView, string.Format(UIResource.Mensagem_Associacao_Grupo_Escalonamento_Vagas_Disponivel, string.Join("<br />", retornoValidacaoVagas.dscVagas)), () => retornoValidacaoVagas.algumasTemVagas);
                }

                this.GrupoEscalonamentoService.SalvarAssociarSolicitacaoGrupoEscalonamento(modelo);

                SetSuccessMessage(UIResource.Mensagem_Associacao_Grupo_Escalonamento, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                this.ThrowRedirect(ex, null);
            }

            return SMCRedirectToUrl(Request.UrlReferrer.ToString());
        }

        [SMCAuthorize(UC_SRC_002_06_05.ASSOCIAR_SOLICITACAO_SERVICO)]
        public ActionResult CabecalhoAssociarSolicitacaoGrupoEscalonamento(long seqGrupoEscalonamento, long seqProcesso)
        {
            var modeloProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<ProcessoCabecalhoViewModel>();

            var modeloCabecalho = modeloProcesso.Transform<GrupoEscalonamentoCabecalhoViewModel>();

            var modeloGrupoEscalonamento = this.GrupoEscalonamentoService.BuscarGrupoEscalonamento(seqGrupoEscalonamento);

            modeloCabecalho.DescricaoGrupoEscalonamento = modeloGrupoEscalonamento.Descricao;

            return PartialView("_CabecalhoAssociarSolicitacaoGrupoEscalonamento", modeloCabecalho);
        }
    }
}