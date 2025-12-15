using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaAtuacaoBloqueioDesbloqueioController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService
        {
            get { return this.Create<IPessoaAtuacaoBloqueioService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_PES_004_03_01.PESQUISAR_BLOQUEIO)]
        public ActionResult BuscarCabecalhoPessoaAtuacaoBloqueio(SMCEncryptedLong seq)
        {
            PessoaAtuacaoBloqueioCabecalhoViewModel model = ExecuteService<PessoaAtuacaoBloqueioCabecalhoData, PessoaAtuacaoBloqueioCabecalhoViewModel>(PessoaAtuacaoBloqueioService.BuscarCabecalhoPessoaAtuacaoBloqueio, seq);
            return PartialView("_Cabecalho", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_PES_004_03_03.DESBLOQUEAR)]
        public ActionResult SalvarPessoaAtuacaoBloqueioDesbloqueio(PessoaAtuacaoBloqueioDesbloqueioDynamicModel model, string backUrl)
        {
            PessoaAtuacaoBloqueioService.SalvarPessoaAtuacaoBloqueioDesbloqueio(model.Transform<PessoaAtuacaoBloqueioDesbloqueioData>());

            if (string.IsNullOrEmpty(backUrl))
                return SMCRedirectToAction("Index", "PessoaAtuacaoBloqueio");
            else
                return SMCRedirectToUrl(backUrl);
        }

    }
}