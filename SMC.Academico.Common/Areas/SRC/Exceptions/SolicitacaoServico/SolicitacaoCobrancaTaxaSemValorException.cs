using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoCobrancaTaxaSemValorException : SMCApplicationException
    {
        public SolicitacaoCobrancaTaxaSemValorException()
           : base(ExceptionsResource.ERR_SolicitacaoCobrancaTaxaSemValorException)
        { }
    }
}
