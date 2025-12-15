using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class PeriodicoArquivoTipoInvalidoException : SMCApplicationException
    {
        public PeriodicoArquivoTipoInvalidoException()
            : base(ExceptionsResource.ERR_PeriodicoArquivoTipoInvalidoException)
        {
        }
    }
}