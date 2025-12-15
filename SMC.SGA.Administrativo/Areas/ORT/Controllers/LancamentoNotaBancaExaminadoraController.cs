using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class LancamentoNotaBancaExaminadoraController : SMCControllerBase
    {
        #region [ Service ]

        private IAplicacaoAvaliacaoService AplicacaoAvaliacaoService
        {
            get => Create<IAplicacaoAvaliacaoService>();
        }

        #endregion [ Service ]
       
        public ActionResult LancarNota(SMCEncryptedLong seqAplicacaoAvaliacao)
        {
            var filtro = new AvaliacaoTrabalhoAcademicoFiltroData() { Seq = seqAplicacaoAvaliacao };
            LancamentoNotaBancaExaminadoraDynamicModel model = AplicacaoAvaliacaoService.BuscarLancamentoNotaBancaExaminadoraInsert(filtro).Transform<LancamentoNotaBancaExaminadoraDynamicModel>();

            return View("Incluir", model);
        }

    }
}