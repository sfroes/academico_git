using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteDivisaoAlteracaoProibidaException : SMCApplicationException
    {
        public ConfiguracaoComponenteDivisaoAlteracaoProibidaException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteDivisaoAlteracaoProibidaException)
        {
        }
    }
}