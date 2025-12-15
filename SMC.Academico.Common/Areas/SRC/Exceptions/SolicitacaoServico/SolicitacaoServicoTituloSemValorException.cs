using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoTituloSemValorException : SMCApplicationException
    {
        public SolicitacaoServicoTituloSemValorException()
           : base(ExceptionsResource.ERR_SolicitacaoServicoTituloSemValorException)
        { }
    }
}
