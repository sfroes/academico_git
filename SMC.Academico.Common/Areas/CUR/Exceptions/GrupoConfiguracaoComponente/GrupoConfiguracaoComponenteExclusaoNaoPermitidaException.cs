using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoComponenteExclusaoNaoPermitidaException : SMCApplicationException
    {
        public GrupoConfiguracaoComponenteExclusaoNaoPermitidaException(string codMatriz, string turma)
            : base(string.Format(ExceptionsResource.ERR_GrupoConfiguracaoComponenteExclusaoNaoPermitidaException, codMatriz, turma))
        {
        }
    }
}
