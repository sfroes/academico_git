using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models.CredenciaisAcesso
{
    public class CredenciaisAcessoViewModel : SMCViewModelBase
    {
        public int? CodigoPessoa { get; set; }
        public string UsuarioPucMail { get; set; }
        public string UsuarioOffice365 { get; set; }
        public string UsuarioEduroam { get; set; }
        public string UsuarioPortalCapes { get; set; }
        public string UsuarioLaboratoriosAcademicos { get; set; }
        public string UsuarioMicrosoftImagine { get; set; }
        public string UsuarioCanvas { get; set; }


        [SMCPassword]
        [SMCMinLength(8)]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        [SMCRequired]
        public string Senha { get; set; }

        [SMCPassword]
        [SMCMinLength(8)]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        [SMCRequired]
        public string ConfirmacaoSenha { get; set; }
    }
}