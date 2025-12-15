using System.Security.Principal;
using System.Threading;

namespace SMC.Academico.Common.Helper
{
    public static class SecurityHelper
    {
        public static void SetupSatUser(string name)
        {
            var identificacao = $@"JOB\{name}";
            var generic = new GenericIdentity(identificacao, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
            generic.AddClaim(new System.Security.Claims.Claim("ApplicationId", "SAT"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);
        }
    }
}
