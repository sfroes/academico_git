using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Views.ProcessoEtapaConfiguracaoNotificacao.App_LocalResources;
using SMC.Notificacoes.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using SMC.Framework.Util;
using SMC.Academico.Common.Areas.SRC.Enums;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ProcessoEtapaConfiguracaoNotificacaoController : SMCControllerBase
    {
        #region Services

        private ITipoNotificacaoService TipoNotificacaoService { get => Create<ITipoNotificacaoService>(); }

        private IProcessoEtapaService ProcessoEtapaService { get => Create<IProcessoEtapaService>(); }

        private IProcessoService ProcessoService { get => Create<IProcessoService>(); }

        private IProcessoEtapaConfiguracaoNotificacaoService ProcessoEtapaConfiguracaoNotificacaoService { get => Create<IProcessoEtapaConfiguracaoNotificacaoService>(); }

        private IProcessoUnidadeResponsavelService ProcessoUnidadeResponsavelService { get => Create<IProcessoUnidadeResponsavelService>(); }

        private IParametroEnvioNotificacaoService ParametroEnvioNotificacaoService { get => Create<IParametroEnvioNotificacaoService>(); }

        private ITipoServicoService TipoServicoService { get => Create<ITipoServicoService>(); }

        private IGrupoEscalonamentoService GrupoEscalonamentoService { get => Create<IGrupoEscalonamentoService>(); }

        #endregion

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult CabecalhoProcesso(SMCEncryptedLong seqProcesso)
        {
            return new ProcessoController().CabecalhoProcesso(seqProcesso, false);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult CabecalhoConfiguracaoNotificacao(long seqProcesso, long seqEtapa)
        {
            var modelCabecalhoProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<ProcessoCabecalhoViewModel>();

            var modelCabecalhoProcessoEtapa = ProcessoEtapaService.BuscarProcessoEtapa(seqEtapa);

            var modelCabecalho = modelCabecalhoProcesso.Transform<EscalonamentoCabecalhoViewModel>();

            modelCabecalho.DescricaoEtapa = modelCabecalhoProcessoEtapa.DescricaoEtapa;
            modelCabecalho.SituacaoEtapa = modelCabecalhoProcessoEtapa.SituacaoEtapa;

            return PartialView("_CabecalhoConfiguracaoNotificacao", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult CabecalhoConfiguracaoNotificacaoParametroEnvio(long seqProcesso, long seqEtapa)
        {
            var modelCabecalhoProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<ProcessoCabecalhoViewModel>();

            var modelCabecalhoProcessoEtapa = ProcessoEtapaService.BuscarProcessoEtapa(seqEtapa);

            var modelCabecalho = modelCabecalhoProcesso.Transform<EscalonamentoCabecalhoViewModel>();

            modelCabecalho.DescricaoEtapa = modelCabecalhoProcessoEtapa.DescricaoEtapa;
            modelCabecalho.SituacaoEtapa = modelCabecalhoProcessoEtapa.SituacaoEtapa;

            var processo = this.ProcessoService.BuscarProcessoEditar(seqProcesso);
            var tipoServico = this.TipoServicoService.BuscarTipoServico(processo.SeqTipoServico);

            modelCabecalho.ExigeEscalonamento = tipoServico.ExigeEscalonamento;

            return PartialView("_CabecalhoConfiguracaoNotificacaoParametroEnvio", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult Index(ProcessoEtapaConfiguracaoNotificacaoFiltroViewModel filtro)
        {
            var processo = this.ProcessoService.BuscarProcessoEditar(filtro.SeqProcesso);
            var tipoServico = this.TipoServicoService.BuscarTipoServico(processo.SeqTipoServico);

            filtro.EntidadesResponsaveis = this.ProcessoUnidadeResponsavelService.BuscarUnidadesResponsaveisPorProcessoSelect(filtro.SeqProcesso);
            filtro.TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoPorServicoSelect(processo.SeqServico);
            filtro.ExigeEscalonamento = tipoServico.ExigeEscalonamento;
            filtro.GruposEscalonamento = this.GrupoEscalonamentoService.BuscarGruposEscalonamentoPorProcessoSelect(filtro.SeqProcesso);

            //VALIDAÇÃO PARA HABILITAR OU NÃO O FILTRO DE GRUPO DE ESCALONAMENTO AO RECARREGAR A PÁGINA 
            if (filtro.ExigeEscalonamento)
            {
                if (filtro.SeqTipoNotificacao.HasValue)
                {
                    var tipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(filtro.SeqTipoNotificacao.Value);

                    if (tipoNotificacao.PermiteAgendamento)
                    {
                        filtro.AuxiliarTipoNotificacaoPermiteAgendamento = true;
                    }
                }
                else if (filtro.PermiteAgendamento.HasValue)
                {
                    if (filtro.PermiteAgendamento.Value)
                    {
                        filtro.AuxiliarPermiteAgendamento = true;
                    }
                }
            }

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public JsonResult PreencherCampoPermiteAgendamento(long? seqTipoNotificacao)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            /*SE O CAMPO TIPO DE NOTIFICAÇÃO FOR PREENCHIDO, O CAMPO PERMITE AGENDAMENTO DEVERÁ SER DESABILITADO E 
             PREENCHIDO DE ACORDO COM O TIPO DE NOTIFICAÇÃO SELECIONADO*/
            if (seqTipoNotificacao.HasValue)
            {
                var tipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(seqTipoNotificacao.Value);

                if (tipoNotificacao.PermiteAgendamento)
                {
                    lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = true });
                    lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
                }
                else
                {
                    lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                    lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
                }
            }
            else
            {
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }

            return Json(lista);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public JsonResult ValidaExibeGrupoEscalonamento(long? seqTipoNotificacao, bool? permiteAgendamento)
        {
            if (seqTipoNotificacao.HasValue)
            {
                var tipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(seqTipoNotificacao.Value);
                return Json(tipoNotificacao.PermiteAgendamento);
            }
            else if (permiteAgendamento.HasValue)
            {
                return Json(permiteAgendamento);
            }

            return Json(false);
        }

        [SMCAuthorize(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult ListarConfiguracaoNotificacao(ProcessoEtapaConfiguracaoNotificacaoFiltroViewModel filtro)
        {
            var retorno = this.ProcessoEtapaConfiguracaoNotificacaoService
                              .BuscarConfiguracaoNotificacaoPorProcesso(filtro.Transform<ProcessoEtapaConfiguracaoNotificacaoFiltroData>());

            var model = retorno.TransformList<ProcessoEtapaConfiguracaoNotificacaoListarViewModel>();

            return PartialView("_List", model);
        }

        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult Incluir(long seqProcesso, long seqEtapa)
        {
            var modelo = new ProcessoEtapaConfiguracaoNotificacaoViewModel() { SeqProcesso = seqProcesso, SeqProcessoEtapa = seqEtapa, EnvioAutomatico = true };

            modelo.TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoPorEtapaServico(seqEtapa);

            return View(modelo);
        }

        /// <summary>
        /// Passo 0 wizard
        /// </summary>
        /// <param name="modelo">Parametros da wizard</param>
        /// <returns></returns>
        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult WizardStepTipoNotificacao(ProcessoEtapaConfiguracaoNotificacaoViewModel modelo)
        {
            if (modelo.SeqTipoNotificacao != 0)
                modelo.DescricaoTipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(modelo.SeqTipoNotificacao).Descricao;

            return PartialView("_WizardStepTipoNotificacao", modelo);
        }

        /// <summary>
        /// Passo 1 wizard
        /// </summary>
        /// <param name="modelo">Parametros da wizard</param>
        /// <returns></returns>
        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult WizardStepConfiguracaoEmail(ProcessoEtapaConfiguracaoNotificacaoViewModel modelo)
        {
            modelo.DescricaoTipoNotificacao = this.TipoNotificacaoService.BuscarTipoNotificacao(modelo.SeqTipoNotificacao).Descricao;
            modelo.UnidadesResponsaveis = this.ProcessoUnidadeResponsavelService.BuscarUnidadesResponsaveisPorProcessoSelect(modelo.SeqProcesso);

            var processo = this.ProcessoService.BuscarProcesso(new ProcessoFiltroData() { Seq = modelo.SeqProcesso });

            if (modelo.ConfiguracaoNotificacao == null)
            {
                modelo.ConfiguracaoNotificacao = new ConfiguracaoNotificacaoEmailViewModel()
                {
                    SeqTipoNotificacao = modelo.SeqTipoNotificacao,
                    DataInicioValidade = DateTime.Now,
                    Descricao = $"{modelo.DescricaoTipoNotificacao} - ({modelo.SeqProcesso}) {processo.Descricao}",
                };
            }
            else
            {
                if (modelo.SeqTipoNotificacaoComparacao > 0 && modelo.SeqTipoNotificacao != modelo.SeqTipoNotificacaoComparacao)
                {
                    modelo.ConfiguracaoNotificacao.SeqTipoNotificacao = modelo.SeqTipoNotificacao;
                    modelo.ConfiguracaoNotificacao.Descricao = $"{modelo.DescricaoTipoNotificacao} - ({modelo.SeqProcesso}) {processo.Descricao}";
                }
            }

            modelo.SeqTipoNotificacaoComparacao = modelo.SeqTipoNotificacao;
            //VARIÁVEL AUXILIAR USADA PARA EXIBIR A DESCRIÇÃO DO TIPO DE NOTIFICAÇÃO EM MEIA COLUNA, AO INVÉS DE USAR O CAMPO 
            //DO COMPONENTE, JÁ QUE NÃO DÁ PARA CONTROLAR O TAMANHO DOS CAMPOS
            modelo.DescricaoTipoNotificacaoAuxiliar = modelo.DescricaoTipoNotificacao;

            return PartialView("_WizardStepConfiguracaoEmail", modelo);
        }

        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult WizardStepConfirmacao(ProcessoEtapaConfiguracaoNotificacaoViewModel modelo)
        {
            return PartialView("_WizardStepConfirmacao", modelo);
        }

        //FIX: Remover o valDep e o ternario ao corrrigir a dependecia do div de wizard em mudanças de passos
        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult BuscarObservacaoTipoNotificacao(long? seqTipoNotificacao, long? valDep)
        {
            seqTipoNotificacao = seqTipoNotificacao ?? valDep;

            var modelo = this.TipoNotificacaoService.BuscarTipoNotificacao(seqTipoNotificacao.GetValueOrDefault()).Transform<TipoNotificacaoViewModel>();

            return PartialView("_ObservacaoTipoNotificacao", modelo);
        }

        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult Salvar(ProcessoEtapaConfiguracaoNotificacaoViewModel modelo)
        {
            long retorno = this.ProcessoEtapaConfiguracaoNotificacaoService.Salvar(modelo.Transform<ProcessoEtapaConfiguracaoNotificacaoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Notificacao, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.ProcessoEtapaConfiguracaoNotificacaoService.BuscarConfiguracaoNotificacao(seq).Transform<ProcessoEtapaConfiguracaoNotificacaoViewModel>();

            var processo = this.ProcessoService.BuscarProcessoEditar(modelo.SeqProcesso);
            modelo.TiposNotificacao = this.TipoNotificacaoService.BuscarTiposNotificacaoPorServicoSelect(processo.SeqServico);

            return View("Editar", modelo);
        }

        [SMCAuthorize(UC_SRC_003_02_02.MANTER_CONFIGURACAO_NOTIFICACAO)]
        public ActionResult Excluir(long seq, long seqProcesso)
        {
            try
            {
                this.ProcessoEtapaConfiguracaoNotificacaoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Notificacao, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [SMCAuthorize(UC_SRC_003_02_03.CONFIGURAR_ENVIO_NOTIFICACAO)]
        public ActionResult ParametroEnvioNotificacao(long seqProcessoEtapaConfiguracaoNotificacao)
        {
            var result = this.ParametroEnvioNotificacaoService.BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(seqProcessoEtapaConfiguracaoNotificacao);

            var modelo = result.Transform<ParametroEnvioNotificacaoViewModel>();

            //FIX: Criado este loop enquanto o Masterdetail modal não poder receber parametro DefaultMode
            foreach (var item in result.ParametroEnvioNotificacaoItem)
            {
                foreach (var parametroEnvio in modelo.ParametroEnvioNotificacaoItem)
                {
                    if (item.Seq == parametroEnvio.Seq)
                    {
                        parametroEnvio.Ativo = item.Ativo;
                    }
                }
            }

            var processo = this.ProcessoService.BuscarProcessoEditar(result.SeqProcesso);
            var tipoServico = this.TipoServicoService.BuscarTipoServico(processo.SeqTipoServico);

            //FIX: Criado este loop enquanto o Masterdetail modal não poder receber parametro DefaultMode
            if (!tipoServico.ExigeEscalonamento)
            {
                modelo.ParametroEnvioNotificacaoItemSemGrupoEscalonamento = modelo.ParametroEnvioNotificacaoItem.TransformMasterDetailList<ParametroEnvioNotificacaoItemSemGrupoEscalonamentoViewModel>();

                foreach (var item in result.ParametroEnvioNotificacaoItem)
                {
                    foreach (var parametroEnvio in modelo.ParametroEnvioNotificacaoItemSemGrupoEscalonamento)
                    {
                        if (item.Seq == parametroEnvio.Seq)
                        {
                            parametroEnvio.Ativo = item.Ativo;
                        }
                    }
                }
            }

            var tipoNotiticacao = this.TipoNotificacaoService.BuscarTipoNotificacao(result.SeqTipoNotificacao);

            /// NV05 - Listar, para seleção, os atributos para agendamento que foram associados ao tipo de notificação em questão.
            List<SMCDatasourceItem> atributosDisponiveis = new List<SMCDatasourceItem>();

            if (tipoNotiticacao.AtributosAgendamento != null)
            {
                foreach (var item in tipoNotiticacao.AtributosAgendamento)
                {
                    atributosDisponiveis.Add(new SMCDatasourceItem() { Seq = (int)item.AtributoAgendamento, Descricao = SMCEnumHelper.GetDescription(item.AtributoAgendamento) });
                }
            }

            modelo.AtributosDisponiveis = atributosDisponiveis;

            return View("ParametroEnvioNotificacao", modelo);
        }

        [SMCAuthorize(UC_SRC_003_02_03.CONFIGURAR_ENVIO_NOTIFICACAO)]
        public ActionResult SalvarParametros(ParametroEnvioNotificacaoViewModel modelo)
        {
            var result = this.ParametroEnvioNotificacaoService.SalvarParametros(modelo.Transform<ParametroEnvioNotificacaoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Parametro_Notificacao, target: SMCMessagePlaceholders.Centro);

            var retorno = result.Transform<ParametroEnvioNotificacaoViewModel>();

            return SMCRedirectToAction(nameof(ParametroEnvioNotificacao), routeValues: new { seqProcessoEtapaConfiguracaoNotificacao = new SMCEncryptedLong(result.SeqProcessoEtapaConfiguracaoNotificacao) });
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult ColocarManutencaoEtapa(SMCEncryptedLong seqEtapa, SMCEncryptedLong seqProcesso)
        {
            var retorno = this.ProcessoEtapaService.ColocarProcessoEtapaManutencao(seqEtapa);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult LiberarEtapa(SMCEncryptedLong seqEtapa, SMCEncryptedLong seqProcesso)
        {
            var retorno = this.ProcessoEtapaService.LiberarProcessoEtapa(seqEtapa);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }
    }
}