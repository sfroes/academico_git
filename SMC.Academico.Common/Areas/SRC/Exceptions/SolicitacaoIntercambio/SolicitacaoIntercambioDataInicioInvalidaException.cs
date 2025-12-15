using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioDataInicioInvalidaException : SMCApplicationException
    {
        public SolicitacaoIntercambioDataInicioInvalidaException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioDataInicioInvalidaException)
        { }
    }
}