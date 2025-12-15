using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoIngressanteFormacaoEspecificaNaoPermitidaException : SMCApplicationException
    {
        public AssociacaoIngressanteFormacaoEspecificaNaoPermitidaException()
            : base(Resources.ExceptionsResource.ERR_AssociacaoIngressanteFormacaoEspecificaNaoPermitida)
        { }
    }
}