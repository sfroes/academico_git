using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.GrupoEscalonamentoItem.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class GrupoEscalonamentoItemController : SMCDynamicControllerBase
    {
        #region Services

        private IMotivoBloqueioService MotivoBloqueioService { get => this.Create<IMotivoBloqueioService>(); }

        private IGrupoEscalonamentoService GrupoEscalonamentoService { get => this.Create<IGrupoEscalonamentoService>(); }
        private IGrupoEscalonamentoItemService GrupoEscalonamentoItemService { get => this.Create<IGrupoEscalonamentoItemService>(); }

        #endregion Services

        [SMCAuthorize(UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO)]
        public ActionResult CabecalhoGrupoEscalonamentoItem(SMCEncryptedLong seq)
        {
            return new ProcessoController().CabecalhoGrupoEscalonamentoItem(seq);
        }

        [SMCAuthorize(UC_SRC_002_06_03.CONFIGURAR_PARCELAS)]
        public ActionResult ValidarTokenMotivoBloqueio(SMCEncryptedLong seqMotivoBloqueio)
        {
            var motivoBolqueio = this.MotivoBloqueioService.BuscarMotivoBloqueio(seqMotivoBloqueio);
            List<SMCSelectListItem> retorno = new List<SMCSelectListItem>();

            if (motivoBolqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PARCELA_SERVICO_ADICIONAL_PENDENTE)
            {
                retorno.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = true });
                retorno.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });

                return Json(retorno);
            }

            retorno.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
            retorno.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });

            return Json(retorno);
        }

        [SMCAuthorize(UC_SRC_002_06_03.CONFIGURAR_PARCELAS)]
        public ActionResult ValidarBloqueioPercentualParcela(long? seqMotivoBloqueio, decimal? valorPercentualBanco)
        {
            if (seqMotivoBloqueio.HasValue)
            {
                var motivoBolqueio = this.MotivoBloqueioService.BuscarMotivoBloqueio(seqMotivoBloqueio.Value);

                /// Se o token do motivo do bloqueio selecionado for igual à PARCELA_SERVICO_ADICIONAL_PENDENTE E exista o valor percentual do serviço adicional 
                /// paramerizado no processo, este campo deve ser preenchido automaticamente com o valor informado no processo e o campo deverá ficar desabilitado.
                if (motivoBolqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PARCELA_SERVICO_ADICIONAL_PENDENTE && (valorPercentualBanco.HasValue && valorPercentualBanco.Value > 0))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }
        }

        [SMCAuthorize(UC_SRC_002_06_03.CONFIGURAR_PARCELAS)]
        public ActionResult PreencherPercentual(bool desativarPercentualParcela, decimal? valorPercentualParcela, decimal? valorPercentualBanco)
        {
            if (desativarPercentualParcela)
            {
                return Json(valorPercentualBanco);
            }
            else
            {
                if (valorPercentualParcela.HasValue)
                    return Json(valorPercentualParcela);
                else
                    return null;
            }
        }


        [SMCAuthorize(UC_SRC_002_06_03.CONFIGURAR_PARCELAS)]
        public ActionResult Editar(SMCEncryptedLong seq, SMCEncryptedLong seqEscalonamento, bool processoExpirado, long NumeroDivisaoParcelas, bool escalonamentoExpirado)
        {
            return PartialView("_DetailEdit");
        }

        [SMCAuthorize(UC_SRC_002_06_03.CONFIGURAR_PARCELAS)]
        public ActionResult ValidarGrupoEscalonamento(SMCEncryptedLong seq, SMCEncryptedLong seqProcesso, SMCEncryptedLong seqGrupoEscalonamento)
        {
            var retorno = this.GrupoEscalonamentoService.ValidarGrupoEscalonamento(seqGrupoEscalonamento);

            SetSuccessMessage(Views.GrupoEscalonamentoItem.App_LocalResources.UIResource.Mensagem_Sucesso_Validar_Grupo, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Editar", "GrupoEscalonamentoItem", routeValues: new { seq = new SMCEncryptedLong(seq), seqProcesso = new SMCEncryptedLong(seqProcesso) });

        }
    }
}