using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Models.CredenciaisAcesso;
using SMC.Academico.UI.Mvc.Areas.PES.Views.CredenciaisAcesso.App_LocalResources;
using SMC.Framework.Exceptions;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Google.Service.ADM;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.PES.Controllers
{
    public class CredenciaisAcessoController : SMCControllerBase
    {
        #region [ Serviços ]

        public IPessoaService PessoaService => Create<IPessoaService>();

        public IGoogleService GoogleService => Create<IGoogleService>();

        #endregion [ Serviços ]

        [SMCAuthorize(UC_PES_001_10_01.CONSULTA_CREDENCIAIS_ACESSO)]
        public ActionResult Index()
        {
            // Recupera o código de pessoa
            var codigoPessoa = User.SMCGetCodigoPessoa();

            if (codigoPessoa == null)
                throw new SMCApplicationException("Código de pessoa não foi vinculado à este usuário.");

            // Recupera endereço de webmail da pessoa (@sga.pucminas.br)
            string emailPessoa = PessoaService.BuscarWebmail(codigoPessoa.GetValueOrDefault());

            var model = new CredenciaisAcessoViewModel
            {
                CodigoPessoa = codigoPessoa,
                UsuarioEduroam = $"{codigoPessoa}@pucminas.br",
                UsuarioLaboratoriosAcademicos = $"{codigoPessoa}",
                UsuarioMicrosoftImagine = UIResource.Info_Credenciais_MicrosoftImagine_Login,
                UsuarioOffice365 = $"{codigoPessoa}{TERMINACAO_EMAIL.PUCMINAS}",
                UsuarioPortalCapes = $"{codigoPessoa}",
                UsuarioPucMail = emailPessoa,
                UsuarioCanvas = $"{codigoPessoa}{TERMINACAO_EMAIL.PUCMINAS}"
            };

            var view = GetExternalView(AcademicoExternalViews.CREDENCIAIS_ACESSO_INDEX);

            return View(view, model);
        }

        [SMCAuthorize(UC_PES_001_10_01.CONSULTA_CREDENCIAIS_ACESSO)]
        public ActionResult SalvarSenhaWebmail(CredenciaisAcessoWebMailViewModel model)
        {
            if (model.Senha != model.ConfirmacaoSenha)
                throw new SMCApplicationException(UIResource.MSG_Erro_Senha_Diferente);
            else
            {
                // Recupera o código de pessoa
                var codigoPessoa = User.SMCGetCodigoPessoa();

                // Recupera o e-mail de acesso ao SGA
                string emailPessoa = PessoaService.BuscarWebmail(codigoPessoa.GetValueOrDefault());
                if (!emailPessoa.EndsWith(TERMINACAO_EMAIL.PUCMINAS))
                    throw new SMCApplicationException(string.Format(UIResource.MSG_Erro_Usuario_Sem_PUCMail, TERMINACAO_EMAIL.PUCMINAS));

                // Chama o serviço para alterar a senha
                GoogleService.ChangeUserPassword(emailPessoa, model.Senha);

                SetSuccessMessage(UIResource.Senha_Alterada_Mensagem, UIResource.Senha_Alterada_Titulo, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "CredenciaisAcessoRoute", new { area = "" });
            }
        }
    }
}