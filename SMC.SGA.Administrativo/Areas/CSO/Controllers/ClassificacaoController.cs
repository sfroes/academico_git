using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class ClassificacaoController : SMCDynamicControllerBase
    {
        #region Serviços

        internal IHierarquiaClassificacaoService HierarquiaClassificacaoService
        {
            get { return this.Create<IHierarquiaClassificacaoService>(); }
        }

        #endregion Serviços

        public ActionResult CabecalhoHierarquiaClassificacao(SMCEncryptedLong seqHierarquiaClassificacao)
        {
            // Busca as informações da hierarquia classificação para o cabeçalho
            HierarquiaClassificacaoCabecalhoViewModel model = ExecuteService<HierarquiaClassificacaoData, HierarquiaClassificacaoCabecalhoViewModel>(HierarquiaClassificacaoService.BuscarHierarquiaClassificacao, seqHierarquiaClassificacao);

            return PartialView("_Cabecalho", model);
        }
    }
}