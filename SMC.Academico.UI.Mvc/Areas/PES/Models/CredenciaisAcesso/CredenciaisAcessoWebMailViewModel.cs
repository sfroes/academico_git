using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models.CredenciaisAcesso
{
    public class CredenciaisAcessoWebMailViewModel : SMCViewModelBase
    {
        public string Senha { get; set; }

        public string ConfirmacaoSenha { get; set; }
    }
}