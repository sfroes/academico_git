using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaAtuacaoBloqueioController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService
        {
            get { return this.Create<IPessoaAtuacaoBloqueioService>(); }
        }

        private IMotivoBloqueioService MotivoBloqueioService
        {
            get { return this.Create<IMotivoBloqueioService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_PES_004_03_02.MANTER_BLOQUEIO)]
        public ActionResult MotivoBloqueioPermiteItens(SMCEncryptedLong seqMotivoBloqueioAuxiliar)
        {
            if (seqMotivoBloqueioAuxiliar == null)
                return Json(false);

            var result = this.MotivoBloqueioService.BuscarMotivoBloqueio(seqMotivoBloqueioAuxiliar);

            return Json(result.PermiteItem);
        }

        /// <summary>
        /// Validar se exibirá o campo descrição ou itens de bloqueio
        /// </summary>
        /// <param name="seqMotivoBloqueio">Seq Motivo de Bloqueio</param>
        /// <param name="permiteItens">Boleano se permite itens</param>
        /// <returns></returns>
        [SMCAuthorize(UC_PES_004_03_02.MANTER_BLOQUEIO)]
        public ActionResult ValidarPermiteDescricaoOuItens(SMCEncryptedLong seqMotivoBloqueioAuxiliar, string permiteItens)
        {
            if (seqMotivoBloqueioAuxiliar != null && permiteItens != null && permiteItens != "")
                return Json(true);
            else
                return Json(false);
        }
    }
}