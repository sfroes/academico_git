using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class ConfiguracaoGradeExclusaoNaoPermitidaException : SMCApplicationException
    {
        public ConfiguracaoGradeExclusaoNaoPermitidaException() :
           base(ExceptionsResource.ERR_ConfiguracaoGradeExclusaoNaoPermitidaException)
        {
        }
    }
}
