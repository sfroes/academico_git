using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Formularios.UI.Mvc.Controller;
using SMC.Framework;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class ProgramaPropostaController<TService, TDynamicModel> : SGFDynamicController<TService, TDynamicModel>
        where TService : ISMCService
        where TDynamicModel : SMCDynamicViewModelBase, new()
    {
        public ProgramaPropostaController() : base()
        {
        }

        public ProgramaPropostaController(Type type) : base(type)
        {
        }

        #region [ Serviços ]

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        #endregion [ Serviços ]

        [SMCAllowAnonymous]
        public ActionResult ProgramaPropostaCabecalho(SMCEncryptedLong seqPrograma)
        {
            var model = ExecuteService<EntidadeCabecalhoData, ProgramaPropostaCabecalhoViewModel>(EntidadeService.BuscarEntidadeCabecalho, seqPrograma);
            return PartialView("_Cabecalho", model);
        }
    }
}