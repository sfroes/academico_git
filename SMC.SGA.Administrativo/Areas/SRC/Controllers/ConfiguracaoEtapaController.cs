using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.ConfiguracaoEtapa.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Framework.Extensions;
using SMC.Academico.Common.Areas.SRC.Enums;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ConfiguracaoEtapaController : SMCControllerBase
    {
        #region [ Services ]

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IProcessoEtapaService ProcessoEtapaService
        {
            get { return this.Create<IProcessoEtapaService>(); }
        }

        private IConfiguracaoProcessoService ConfiguracaoProcessoService
        {
            get { return this.Create<IConfiguracaoProcessoService>(); }
        }

        private IConfiguracaoEtapaService ConfiguracaoEtapaService
        {
            get { return this.Create<IConfiguracaoEtapaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_002_04_01.PESQUISAR_CONFIGURACAO_ETAPA)]
        public ActionResult Index(ConfiguracaoEtapaFiltroViewModel filtro)
        {
            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_04_01.PESQUISAR_CONFIGURACAO_ETAPA)]
        public ActionResult CabecalhoIndex(long seqProcesso)
        {
            var modeloProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<CabecalhoConfiguracaoEtapaViewModel>();
            return PartialView("_CabecalhoIndex", modeloProcesso);
        }

        [SMCAuthorize(UC_SRC_002_04_01.PESQUISAR_CONFIGURACAO_ETAPA)]
        public ActionResult ListarConfiguracoesEtapa(ConfiguracaoEtapaFiltroViewModel filtro)
        {
            SMCPagerModel<ConfiguracaoEtapaListarViewModel> model = ExecuteService<ConfiguracaoEtapaFiltroData, ConfiguracaoEtapaListarData,
                                                                         ConfiguracaoEtapaFiltroViewModel, ConfiguracaoEtapaListarViewModel>
                                                                         (ConfiguracaoEtapaService.BuscarConfiguracoesEtapa, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult LiberarEtapa(long seqProcesso, long seqProcessoEtapa)
        {
            var retorno = this.ProcessoEtapaService.LiberarProcessoEtapa(seqProcessoEtapa);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult ColocarManutencaoEtapa(long seqProcesso, long seqProcessoEtapa)
        {
            var retorno = this.ProcessoEtapaService.ColocarProcessoEtapaManutencao(seqProcessoEtapa);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult Incluir(long seqProcesso, long seqProcessoEtapa)
        {
            var modelo = new ConfiguracaoEtapaViewModel()
            {
                SeqProcessoEtapa = seqProcessoEtapa,
                SeqProcesso = seqProcesso,
                ConfiguracoesProcesso = this.ConfiguracaoProcessoService.BuscarConfiguracoesProcessoSelect(new ConfiguracaoProcessoFiltroData() { SeqProcesso = seqProcesso }),
                CamposReadyOnly = false,
                CampoOrientacaoReadOnly = true
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult ManipularDescricao(long seqProcessoEtapa, long? seqConfiguracaoProcesso)
        {
            string retorno = "";

            if (seqConfiguracaoProcesso.HasValue)
            {
                var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(seqProcessoEtapa);
                var configuracaoProcesso = this.ConfiguracaoProcessoService.BuscarConfiguracaoProcesso(seqConfiguracaoProcesso.Value);

                retorno = $"{processoEtapa.Ordem}° Etapa - {configuracaoProcesso.Descricao}";
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seq, IncludesConfiguracaoEtapa.Nenhum).Transform<ConfiguracaoEtapaViewModel>();

            var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(modelo.SeqProcessoEtapa);

            // Ao abrir a tela no modo edição: 
            // Se a etapa estiver Encerrada ou Liberada, todos os campos devem ser desabilitados.
            // Senão, somente os campos "Orientação" e "Termo de responsabilidade da entrega da documentação" 
            // deverão ser exibidos habilitados. (48839)

            var situacaoEtapaEncerradaLIberada = processoEtapa.SituacaoEtapa == SituacaoEtapa.Liberada || processoEtapa.SituacaoEtapa == SituacaoEtapa.Encerrada;

            modelo.CamposReadyOnly = situacaoEtapaEncerradaLIberada;

            modelo.CampoOrientacaoReadOnly = !situacaoEtapaEncerradaLIberada;

            modelo.SeqProcesso = processoEtapa.SeqProcesso;
            modelo.ConfiguracoesProcesso = this.ConfiguracaoProcessoService.BuscarConfiguracoesProcessoSelect(new ConfiguracaoProcessoFiltroData() { SeqProcesso = processoEtapa.SeqProcesso });

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult CabecalhoManter(long seqProcessoEtapa)
        {
            var modelCabecalho = ExecuteService<ProcessoEtapaCabecalhoData, CabecalhoConfiguracaoEtapaViewModel>(ProcessoEtapaService.BuscarCabecalhoProcessoEtapa, seqProcessoEtapa);

            return PartialView("_CabecalhoManter", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult Salvar(ConfiguracaoEtapaViewModel modelo)
        {
            long retorno = this.ConfiguracaoEtapaService.Salvar(modelo.Transform<ConfiguracaoEtapaData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Etapa, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_SRC_002_04_02.MANTER_CONFIGURACAO_ETAPA)]
        public ActionResult Excluir(long seq, long seqProcesso)
        {
            try
            {
                this.ConfiguracaoEtapaService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Configuracao_Etapa, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcesso = new SMCEncryptedLong(seqProcesso) });
        }
    }
}