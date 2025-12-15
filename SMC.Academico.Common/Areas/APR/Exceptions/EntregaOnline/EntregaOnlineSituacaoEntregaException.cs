using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class EntregaOnlineSituacaoEntregaException : SMCApplicationException
    {
        public EntregaOnlineSituacaoEntregaException()
            : base(ExceptionsResource.ERR_EntregaOnlineSituacaoEntregaException)
        {
        }
    }
}