using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Academico.UI.Mvc.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Localidades.UI.Mvc.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.PES.Controllers
{
    public class PessoaEnderecoController : SMCControllerBase
    {
        #region [ Serviços ]

        private IPessoaEnderecoService PessoaEnderecoService
        {
            get { return this.Create<IPessoaEnderecoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAllowAnonymous]
        public ActionResult Salvar(PessoaEnderecoViewModel model)
        {
            PessoaEnderecoData enderecoData = model.Transform<PessoaEnderecoData>();
            enderecoData.Endereco = model.Endereco?.FirstOrDefault().Transform<EnderecoData>();

            PessoaEnderecoService.SalvarEnderecoPessoa(enderecoData);
            return null;
        }

        [SMCAllowAnonymous]
        public ActionResult Editar(SMCEncryptedLong seq)
        {
            var data = PessoaEnderecoService.BuscarPessoaEndereco(seq);
            var model = data.Transform<PessoaEnderecoViewModel>();
            model.Endereco = new Localidades.UI.Mvc.Models.AddressList();
            var endereco = SMCMapperHelper.Create<InformacoesEnderecoViewModel>(data.Endereco);
            model.Endereco.Add(endereco);

            var view = GetExternalView(AcademicoExternalViews.ENDERECO_EDITAR);
            return PartialView(view, model);
        }

        [SMCAllowAnonymous]
        public ActionResult Incluir(SMCEncryptedLong seqPessoa)
        {
            var model = new PessoaEnderecoViewModel { SeqPessoa = seqPessoa };

            var view = GetExternalView(AcademicoExternalViews.ENDERECO_INCLUIR);
            return PartialView(view, model);
        }
    }
}