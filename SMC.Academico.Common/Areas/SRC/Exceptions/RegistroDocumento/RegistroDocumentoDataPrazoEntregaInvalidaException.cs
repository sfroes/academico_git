using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoDataPrazoEntregaInvalidaException : SMCApplicationException
    {
        public RegistroDocumentoDataPrazoEntregaInvalidaException()
            : base(string.Format(ExceptionsResource.ERR_RegistroDocumentoDataPrazoEntregaInvalida, DateTime.Now.ToString("dd/MM/yyyy")))

        {
        }
    }
}