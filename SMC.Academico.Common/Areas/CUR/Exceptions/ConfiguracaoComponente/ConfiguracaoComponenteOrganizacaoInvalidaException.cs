using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteOrganizacaoInvalidaException : SMCApplicationException
    {
        public ConfiguracaoComponenteOrganizacaoInvalidaException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteOrganizacaoInvalidaException)
        {
        }
    }
}