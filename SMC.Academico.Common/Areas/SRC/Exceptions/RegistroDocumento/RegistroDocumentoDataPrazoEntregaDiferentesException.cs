using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoDataPrazoEntregaDiferentesException : SMCApplicationException
    {
        public RegistroDocumentoDataPrazoEntregaDiferentesException()
            : base(ExceptionsResource.ERR_RegistroDocumentoDataPrazoEntregaDiferentes)

        {
        }
    }
}