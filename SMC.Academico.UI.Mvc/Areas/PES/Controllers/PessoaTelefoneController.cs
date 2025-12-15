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
    public class PessoaTelefoneController : SMCControllerBase
    {
        #region [ Serviços ]

        private IPessoaTelefoneService PessoaTelefoneService
        {
            get { return this.Create<IPessoaTelefoneService>(); }
        }

        #endregion [ Serviços ]

        [SMCAllowAnonymous]
        public ActionResult Salvar(PessoaTelefoneViewModel model)
        {
            var telefoneData = model.Transform<PessoaTelefoneData>();
            telefoneData.Telefone = model.Telefone?.FirstOrDefault().Transform<TelefoneData>();

            PessoaTelefoneService.SalvarTelefonePessoa(telefoneData);
            return null;
        }

        [SMCAllowAnonymous]
        public ActionResult Editar(SMCEncryptedLong seq)
        {
            var data = PessoaTelefoneService.BuscarPessoaTelefone(seq);
            var model = data.Transform<PessoaTelefoneViewModel>();
            model.Telefone = new PhoneList();
            var telefone = SMCMapperHelper.Create<InformacoesTelefoneViewModel>(data.Telefone);
            model.Telefone.Add(telefone);

            var view = GetExternalView(AcademicoExternalViews.TELEFONE_EDITAR);
            return PartialView(view, model);
        }

        [SMCAllowAnonymous]
        public ActionResult Incluir(SMCEncryptedLong seqPessoa)
        {
            var model = new PessoaTelefoneViewModel { SeqPessoa = seqPessoa };

            var view = GetExternalView(AcademicoExternalViews.TELEFONE_INCLUIR);
            return PartialView(view, model);
        }
    }
}