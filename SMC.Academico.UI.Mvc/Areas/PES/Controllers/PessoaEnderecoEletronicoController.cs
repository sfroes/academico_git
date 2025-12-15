using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.PES.Controllers
{
    public class PessoaEnderecoEletronicoController : SMCControllerBase
    {
        #region [ Serviços ]

        private IPessoaEnderecoEletronicoService PessoaEnderecoEletronicoService
        {
            get { return this.Create<IPessoaEnderecoEletronicoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAllowAnonymous]
        public ActionResult Salvar(PessoaEnderecoEletronicoViewModel model)
        {
            var enderecoEletronicoData = model.Transform<PessoaEnderecoEletronicoData>();

            PessoaEnderecoEletronicoService.SalvarEnderecoEletronicoPessoa(enderecoEletronicoData);
            return null;
        }

        [SMCAllowAnonymous]
        public ActionResult Editar(SMCEncryptedLong seq)
        {
            var data = PessoaEnderecoEletronicoService.BuscarPessoaEnderecoEletronico(seq);
            var model = data.Transform<PessoaEnderecoEletronicoViewModel>();

            var view = GetExternalView(AcademicoExternalViews.ENDERECO_ELETRONICO_EDITAR);
            return PartialView(view, model);
        }

        [SMCAllowAnonymous]
        public ActionResult Incluir(SMCEncryptedLong seqPessoa)
        {
            var model = new PessoaEnderecoEletronicoViewModel { SeqPessoa = seqPessoa };

            var view = GetExternalView(AcademicoExternalViews.ENDERECO_ELETRONICO_INCLUIR);
            return PartialView(view, model);
        }
    }
}