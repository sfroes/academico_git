using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService { get => Create<IPessoaAtuacaoService>(); }

        #endregion [ Services ]

        /// <summary>
        /// BI_PES_004 - Pessoa - Cabeçalho
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do ingressante</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult CabecalhoIngressante(SMCEncryptedLong seqPessoaAtuacao)
        {
            var modelHeader = ExecuteService<PessoaAtuacaoCabecalhoData, AssociacaoIngressanteLoteCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho, seqPessoaAtuacao);
            return PartialView("_Cabecalho", modelHeader);
        }
    }
}