using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class PeriodicoArquivoInvalidoException : SMCApplicationException
    {
        public PeriodicoArquivoInvalidoException()
            : base(ExceptionsResource.ERR_PeriodicoArquivoInvalidoException)
        {
        }
    }
}