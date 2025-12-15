using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.SGA.Administrativo.Areas.SRC.Views.Processo.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ProcessoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IProcessoService ProcessoService
        {
            get { return Create<IProcessoService>(); }
        }

        private IServicoService ServicoService
        {
            get { return Create<IServicoService>(); }
        }

        private ITipoServicoService TipoServicoService
        {
            get { return Create<ITipoServicoService>(); }
        }

        private IProcessoUnidadeResponsavelService ProcessoUnidadeResponsavelService
        {
            get { return Create<IProcessoUnidadeResponsavelService>(); }
        }

        private IProcessoEtapaService ProcessoEtapaService { get => Create<IProcessoEtapaService>(); }

        #endregion [ Services ]

        public ActionResult CabecalhoProcesso(SMCEncryptedLong seqProcesso, bool exibirQuantidade)
        {
            var modelCabecalho = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, exibirQuantidade).Transform<ProcessoCabecalhoViewModel>();
            return PartialView("_Cabecalho", modelCabecalho);
        }

        public ActionResult CabecalhoGrupoEscalonamentoItem(SMCEncryptedLong seqGrupoEscalonamentoItem)
        {
            var modelCabecalho = ProcessoService.BuscarCabecalhoGrupoEscalonamentoItem(seqGrupoEscalonamentoItem).Transform<GrupoEscalonamentoItemCabecalhoViewModel>();
            return PartialView("_Cabecalho", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_01_02.MANTER_PROCESSO)]
        public ActionResult BuscarTokenTipoServico(SMCEncryptedLong seqTipoServico)
        {
            return Content(this.TipoServicoService.BuscarTipoServico(seqTipoServico).Token);
        }

        [SMCAuthorize(UC_SRC_002_01_02.MANTER_PROCESSO)]
        public ActionResult BuscarTokenServico(SMCEncryptedLong seqServico)
        {
            return Content(this.ServicoService.BuscarServico(seqServico).Token);
        }

        [SMCAuthorize(UC_SRC_002_01_01.PESQUISAR_PROCESSOS)]
        public ActionResult BuscarServicosPorTipoServicoSelect(long seqTipoServico)
        {
            return Json(ServicoService.BuscarServicosPorTipoServicoSelect(seqTipoServico));
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult ColocarManutencaoEtapa(SMCEncryptedLong seqEtapa)
        {
            var retorno = this.ProcessoEtapaService.ColocarProcessoEtapaManutencao(seqEtapa);

            return SMCRedirectToAction("Index", "Processo");
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult LiberarEtapa(SMCEncryptedLong seqEtapa)
        {
            var retorno = this.ProcessoEtapaService.LiberarProcessoEtapa(seqEtapa);

            return SMCRedirectToAction("Index", "Processo");
        }

        [SMCAuthorize(UC_SRC_002_01_01.ENCERRAR_PROCESSO)]
        public ActionResult EncerrarProcesso(SMCEncryptedLong seqProcesso)
        {
            try
            {
                ProcessoService.EncerrarProcesso(seqProcesso);
                SetSuccessMessage("Processo encerrado com sucesso!", null, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, null, SMCMessagePlaceholders.Centro);
            }
            return RedirectToAction("Index", "Processo");
        }

        [SMCAuthorize(UC_SRC_002_01_02.MANTER_PROCESSO)]
        public ActionResult BuscarEtapasSGFPorServico(SMCEncryptedLong seqServico)
        {
            var model = new ProcessoDynamicModel()
            {
                EtapasSGF = ProcessoService.BuscarEtapasSGFPorServico(seqServico).TransformList<ProcessoEtapaSGFViewModel>()
            };

            return PartialView("_EtapasSGF", model);
        }

        [SMCAuthorize(UC_SRC_002_01_04.COPIAR_PROCESSO)]
        public ActionResult CopiarProcesso(SMCEncryptedLong seqProcesso)
        {
            var modelo = this.ProcessoService.BuscarProcessoCopiar(seqProcesso).Transform<CopiarProcessoViewModel>();

            //SETANDO OS VALORES DEFAULT DAS ETAPAS
            foreach (var etapa in modelo.EtapasCopiar)
            {
                if (etapa.Obrigatoria)
                    etapa.CopiarConfiguracoes = true;
                
                etapa.SituacaoEtapa = SituacaoEtapa.AguardandoLiberacao;

                etapa.DataInicio = null;
                etapa.DataFim = null;
            }

            foreach (var grupo in modelo.GruposEscalonamentoCopiar)
            {
                grupo.CopiarNotificacoes = true;
            }

            if (modelo.EtapasCopiar.Any(a => a.TipoPrazoEtapa != TipoPrazoEtapa.Escalonamento))
                modelo.MensagemInformativa = string.Format(UIResource.MSG_CopiarProcesso, "<p>");
            else
                modelo.MensagemInformativa = string.Format(UIResource.MSG_CopiarProcessoPrazoEscalonamento, "<p>");

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_002_01_04.COPIAR_PROCESSO)]
        public JsonResult PreencherCampoAssociar(bool? obrigatoria)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            //SE A ETAPA FOR OBRIGATÓRIA, O CAMPO ASSOCIAR SERÁ PREENCHIDO COM SIM, E O VALOR DEFAULT PARA O CAMPO TAMBÉM SERÁ SIM
            lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = true });
            lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });

            return Json(lista);
        }

        [SMCAuthorize(UC_SRC_002_01_04.COPIAR_PROCESSO)]
        public JsonResult PreencherCampoCopiarNotificacoes(bool? criarGrupo)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();

            if (criarGrupo.HasValue && !criarGrupo.Value)
            {
                //SE COPIAR GRUPO FOR IGUAL A NÃO, O CAMPO COPIAR NOTIFICAÇÕES DEVERÁ SER DESABILITADO E PREENCHIDO COM A OPÇÃO NÃO
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
            }
            else
            {
                //SE COPIAR GRUPO FOR IGUAL A NÃO, O CAMPO COPIAR NOTIFICAÇÕES DEVERÁ SER DESABILITADO E PREENCHIDO COM A OPÇÃO NÃO
                lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }

            return Json(lista);
        }

        [SMCAuthorize(UC_SRC_002_01_04.COPIAR_PROCESSO)]
        public ActionResult SalvarCopiaProcesso(CopiarProcessoViewModel modelo)
        {
            long retorno = ProcessoService.SalvarCopiaProcesso(modelo.Transform<CopiarProcessoData>());
            SetSuccessMessage("Processo copiado com sucesso!", null, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Editar", "Processo", routeValues: new { Seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_SRC_002_01_01.REABRIR_PROCESSO)]
        public ActionResult ReabrirProcesso(SMCEncryptedLong seqProcesso)
        {
            try
            {
                ProcessoService.ReabrirProcesso(seqProcesso);
                SetSuccessMessage("Processo reaberto com sucesso!", null, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, null, SMCMessagePlaceholders.Centro);
            }
            return RedirectToAction("Index", "Processo");
        }
    }
}