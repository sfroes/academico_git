using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class EscalaApuracaoSequenciaIncorretaPercentuaisException : SMCApplicationException
    {
        public EscalaApuracaoSequenciaIncorretaPercentuaisException()
            : base(ExceptionsResource.ERR_EscalaApuracaoSequenciaIncorretaPercentuais)
        {
        }
    }
}