using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteComSituacaoMatriculaInvalidaException : SMCApplicationException
    {
        public IngressanteComSituacaoMatriculaInvalidaException(string situacao)
            : base(string.Format(ExceptionsResource.ERR_IngressanteComSituacaoMatriculaInvalidaException, situacao))
        { }
    }
}