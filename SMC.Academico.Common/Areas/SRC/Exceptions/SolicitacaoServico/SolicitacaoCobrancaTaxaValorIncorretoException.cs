using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoCobrancaTaxaValorIncorretoException : SMCApplicationException
    {
        public SolicitacaoCobrancaTaxaValorIncorretoException()
           : base(ExceptionsResource.ERR_SolicitacaoCobrancaTaxaValorIncorretoException)
        { }
    }
}
