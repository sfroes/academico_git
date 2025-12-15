using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoNotaMaximaExecedenteException : SMCApplicationException
    {
        public AvaliacaoNotaMaximaExecedenteException(string notaMaxima)
            : base(string.Format(ExceptionsResource.ERR_AvaliacaoNotaMaximaExecedenteException, notaMaxima))
        {
        }
    }
}