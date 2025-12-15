using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoObrigatoriaFormacaoEspecificaIngressanteException : SMCApplicationException
    {
        public AssociacaoObrigatoriaFormacaoEspecificaIngressanteException()
            : base(Resources.ExceptionsResource.ERR_AssociacaoObrigatoriaFormacaoEspecificaIngressante)
        { }
    }
}