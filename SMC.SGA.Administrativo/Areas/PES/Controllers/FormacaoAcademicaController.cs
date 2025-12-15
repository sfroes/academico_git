using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class FormacaoAcademicaController : SMCDynamicControllerBase
    {
        #region [ Services ]
        IPessoaAtuacaoService PessoaAtuacaoService { get => Create<IPessoaAtuacaoService>(); }
        private IFormacaoAcademicaService FormacaoAcademicaService { get => Create<IFormacaoAcademicaService>(); }
        private ITitulacaoDocumentoComprobatorioService TitulacaoDocumentoCompService { get => Create<ITitulacaoDocumentoComprobatorioService>(); }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult CabecalhoFormacaoAcademica(SMCEncryptedLong seqPessoaAtuacao)
        {
            var model = ExecuteService<FormacaoAcademicaCabecalhoData, CabecalhoFormacaoAcademicaViewModel>(FormacaoAcademicaService.BuscarFormacaoAcademicaCabecalho, (long)seqPessoaAtuacao);

            return PartialView("_Cabecalho", model);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarDocumentosComprobatoriosTitulacao(long SeqTitulacao)
        {
            return Json(TitulacaoDocumentoCompService.BuscarTitulacaoDocumentosComprobatorios(SeqTitulacao));
        }
        
    }
}