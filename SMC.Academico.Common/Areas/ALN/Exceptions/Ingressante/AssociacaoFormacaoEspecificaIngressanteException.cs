using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoFormacaoEspecificaIngressanteException : SMCApplicationException
    {
        public AssociacaoFormacaoEspecificaIngressanteException(string permissoes)
            : base(string.Format(Resources.ExceptionsResource.ERR_AssociacaoFormacaoEspecificaIngressante, permissoes))
        { }
    }
}