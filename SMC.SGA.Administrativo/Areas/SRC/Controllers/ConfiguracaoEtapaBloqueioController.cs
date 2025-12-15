using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Extensions;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.ConfiguracaoEtapaBloqueio.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using System;
using SMC.Framework;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ConfiguracaoEtapaBloqueioController : SMCControllerBase
    {
        #region [ Services ]   

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IConfiguracaoEtapaService ConfiguracaoEtapaService
        {
            get { return this.Create<IConfiguracaoEtapaService>(); }
        }

        private IConfiguracaoEtapaBloqueioService ConfiguracaoEtapaBloqueioService
        {
            get { return this.Create<IConfiguracaoEtapaBloqueioService>(); }
        }

        private IProcessoEtapaService ProcessoEtapaService
        {
            get { return this.Create<IProcessoEtapaService>(); }
        }

        private IMotivoBloqueioService MotivoBloqueioService
        {
            get { return this.Create<IMotivoBloqueioService>(); }
        }

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return this.Create<ISolicitacaoServicoService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_002_04_08.CONFIGURAR_ETAPA_PESQUISAR_BLOQUEIOS)]
        public ActionResult Index(ConfiguracaoEtapaBloqueioFiltroViewModel filtro)
        {
            var processoEtapa = this.ProcessoEtapaService.BuscarProcessoEtapa(filtro.SeqProcessoEtapa).Transform<ProcessoEtapaViewModel>();

            filtro.SituacaoEtapa = processoEtapa.SituacaoEtapa;
            filtro.SeqProcesso = processoEtapa.SeqProcesso;

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_002_04_08.CONFIGURAR_ETAPA_PESQUISAR_BLOQUEIOS)]
        public ActionResult Cabecalho(long seqConfiguracaoEtapa)
        {
            var modeloCabecalho = ConfiguracaoEtapaService.BuscarCabecalhoConfiguracaoEtapa(seqConfiguracaoEtapa).Transform<CabecalhoConfiguracaoEtapaBloqueioViewModel>();
            return PartialView("_Cabecalho", modeloCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_04_08.CONFIGURAR_ETAPA_PESQUISAR_BLOQUEIOS)]
        public ActionResult ListarConfiguracoesEtapaBloqueio(ConfiguracaoEtapaBloqueioFiltroViewModel filtro)
        {
            SMCPagerModel<ConfiguracaoEtapaBloqueioListarViewModel> model = ExecuteService<ConfiguracaoEtapaBloqueioFiltroData, ConfiguracaoEtapaBloqueioListarData,
                                                                                   ConfiguracaoEtapaBloqueioFiltroViewModel, ConfiguracaoEtapaBloqueioListarViewModel>
                                                                                  (ConfiguracaoEtapaBloqueioService.BuscarConfiguracoesEtapaBloqueio, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public ActionResult Incluir(long seqConfiguracaoEtapa)
        {
            var configuracaoEtapa = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ProcessoEtapa);

            var modelo = new ConfiguracaoEtapaBloqueioViewModel()
            {
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                DescricaoConfiguracaoEtapa = configuracaoEtapa.Descricao,
                SeqProcessoEtapa = configuracaoEtapa.SeqProcessoEtapa,
                DescricaoEtapaSgf = configuracaoEtapa.ProcessoEtapa.DescricaoEtapa,
                MotivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioDescricaoCompletaPorInstituicaoSelect(HttpContext.GetInstituicaoEnsinoLogada().Seq),
                SolicitacaoAssociada = this.SolicitacaoServicoService.VerificarProcessoPossuiSolicitacaoServico(configuracaoEtapa.ProcessoEtapa.SeqProcesso)
            };

            return PartialView("_DadosConfiguracaoEtapaBloqueio", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.ConfiguracaoEtapaBloqueioService.BuscarConfiguracaoEtapaBloqueio(seq).Transform<ConfiguracaoEtapaBloqueioViewModel>();
            modelo.MotivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioDescricaoCompletaPorInstituicaoSelect(HttpContext.GetInstituicaoEnsinoLogada().Seq);

            return PartialView("_DadosConfiguracaoEtapaBloqueio", modelo);
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public ActionResult Salvar(ConfiguracaoEtapaBloqueioViewModel modelo)
        {
            this.ConfiguracaoEtapaBloqueioService.ValidarModeloSalvar(modelo.Transform<ConfiguracaoEtapaBloqueioData>());

            /********************** VALIDAÇÕES DE ASSERT **********************/

            if (modelo.SolicitacaoAssociada)
            {
                if (modelo.GeraCancelamentoSolicitacao.Value || modelo.ImpedeInicioEtapa.Value || modelo.ImpedeFimEtapa.Value)
                    Assert(modelo, string.Format(UIResource.MSG_Assert_SalvarBloqueio, modelo.DescricaoEtapaSgf, modelo.DescricaoMotivo));
            }

            /********************** FIM VALIDAÇÕES DE ASSERT **********************/

            long retorno = this.ConfiguracaoEtapaBloqueioService.Salvar(modelo.Transform<ConfiguracaoEtapaBloqueioData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Etapa_Bloqueio, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(modelo.SeqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(modelo.SeqConfiguracaoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public ActionResult Excluir(long seq, long seqProcessoEtapa, long seqConfiguracaoEtapa)
        {
            try
            {
                this.ConfiguracaoEtapaBloqueioService.ValidarModeloExcluir(seq);

                /********************** VALIDAÇÕES DE ASSERT **********************/

                var modelo = this.ConfiguracaoEtapaBloqueioService.BuscarConfiguracaoEtapaBloqueio(seq).Transform<ConfiguracaoEtapaBloqueioViewModel>();

                if (modelo.SolicitacaoAssociada && modelo.GeraCancelamentoSolicitacao.Value)
                    Assert(modelo, string.Format(UIResource.MSG_Assert_ExcluirBloqueioComCancelamento, modelo.DescricaoEtapaSgf, modelo.DescricaoMotivo));
                else if (modelo.SolicitacaoAssociada && !modelo.GeraCancelamentoSolicitacao.Value && modelo.ImpedeFimEtapa.Value)
                    Assert(modelo, string.Format(UIResource.MSG_Assert_ExcluirBloqueioSemCancelamentoImpedeFimEtapa, modelo.DescricaoEtapaSgf, modelo.DescricaoMotivo));
                else
                    Assert(modelo, string.Format(UIResource.Exclusao_Configuracao_Etapa_Bloqueio_Confirmacao, modelo.DescricaoMotivo));

                /********************** FIM VALIDAÇÕES DE ASSERT **********************/

                this.ConfiguracaoEtapaBloqueioService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Configuracao_Etapa_Bloqueio, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqProcessoEtapa = new SMCEncryptedLong(seqProcessoEtapa), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public JsonResult PreencherCampoImpedeInicioEtapa(bool? geraCancelamentoSolicitacao)
        {
            return Json(RetornaSelectImpedeInicioFimEtapa(geraCancelamentoSolicitacao));
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public JsonResult PreencherCampoImpedeFimEtapa(bool? geraCancelamentoSolicitacao)
        {
            return Json(RetornaSelectImpedeInicioFimEtapa(geraCancelamentoSolicitacao));
        }

        [SMCAuthorize(UC_SRC_002_04_09.CONFIGURAR_ETAPA_MANTER_BLOQUEIOS)]
        public List<SMCSelectListItem> RetornaSelectImpedeInicioFimEtapa(bool? geraCancelamentoSolicitacao)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            if (geraCancelamentoSolicitacao.HasValue && geraCancelamentoSolicitacao.Value)
            {
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = true });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }
            else
            {
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }

            return lista;
        }
    }
}