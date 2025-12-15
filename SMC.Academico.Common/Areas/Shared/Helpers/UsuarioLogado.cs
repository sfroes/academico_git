using SMC.Framework;
using SMC.Framework.Security;

namespace SMC.Academico.Common.Areas.Shared.Helpers
{
    public class UsuarioLogado
    {
        public static string RetornaUsuarioLogado()
        {
            var usuarioInclusao = string.Empty;
            var sequencialUsuario = SMCContext.User?.SMCGetSequencialUsuario();
            var nomeReduzido = SMCContext.User?.SMCGetNomeReduzido();

            if (sequencialUsuario != null && nomeReduzido != null)
                usuarioInclusao = $"{sequencialUsuario}/{nomeReduzido.ToUpper()}";
            else
                usuarioInclusao = SMCContext.User?.Identity?.Name;

            return usuarioInclusao;
        }
    }
}
