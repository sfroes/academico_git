using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class FuncionarioVinculoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        #endregion [ Services ]

        public ActionResult CabecalhoFuncionarioVinculo(SMCEncryptedLong seqFuncionario)
        {
            var model = PessoaAtuacaoService.BuscarPessoaAtuacao(seqFuncionario).Transform<PessoaAtuacaoCabecalhoViewModel>();

            return PartialView("_Cabecalho", model);
        }
    }
}