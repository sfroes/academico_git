using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoComEtapaNãoLiberadaException : SMCApplicationException
    {
        public SolicitacaoServicoComEtapaNãoLiberadaException(string situacao)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoComEtapaNãoLiberadaException, situacao))
        { }
    }
}