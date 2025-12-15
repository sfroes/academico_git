using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioConfiguracaoNaoEncontradaException : SMCApplicationException
    {
        public SolicitacaoIntercambioConfiguracaoNaoEncontradaException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioConfiguracaoNaoEncontradaException)
        { }
    }
}