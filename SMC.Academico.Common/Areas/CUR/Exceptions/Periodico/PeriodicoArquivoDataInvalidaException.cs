using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class PeriodicoArquivoDataInvalidaException : SMCApplicationException
    {
        public PeriodicoArquivoDataInvalidaException()
            : base(ExceptionsResource.ERR_PeriodicoArquivoDataInvalidaException)
        {
        }
    }
}