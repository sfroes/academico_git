using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class TipoClassificacaoController : SMCDynamicControllerBase
    {
        #region Serviços

        internal ITipoHierarquiaClassificacaoService TipoHierarquiaClassificacaoService
        {
            get { return this.Create<ITipoHierarquiaClassificacaoService>(); }
        }

        internal ITipoClassificacaoService TipoClassificacaoService
        {
            get { return this.Create<ITipoClassificacaoService>(); }
        }

        #endregion Serviços

        public ActionResult CabecalhoTipoHierarquiaClassificacao(SMCEncryptedLong seqTipoHierarquiaClassificacao)
        {
            // Busca as informações do Tipo hierarquia entidade para o cabeçalho
            TipoHierarquiaClassificacaoCabecalhoViewModel model = ExecuteService<TipoHierarquiaClassificacaoData, TipoHierarquiaClassificacaoCabecalhoViewModel>(TipoHierarquiaClassificacaoService.BuscarTipoHierarquiaClassificacao, seqTipoHierarquiaClassificacao);
            return PartialView("_Cabecalho", model);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarTiposClassificacaoPorHierarquiaSelect(long seqHierarquiaClassificacao)
        {
            try
            {
                List<SMCDatasourceItem> itens = TipoClassificacaoService.BuscarTiposClassificacaoPorHierarquiaSelect(new TipoClassificacaoPorHierarquiaFiltroData() { SeqHierarquiaClassificacao = seqHierarquiaClassificacao, Exclusivo = false, SeqClassificacaoSuperior = null });
                return Json(itens);
            }
            catch
            {
                return Json(new List<SMCDatasourceItem>());
            }
        }
    }
}