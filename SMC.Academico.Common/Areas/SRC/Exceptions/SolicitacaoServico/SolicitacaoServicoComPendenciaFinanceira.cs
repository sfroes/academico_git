using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoComPendenciaFinanceira : SMCApplicationException
    {
        public SolicitacaoServicoComPendenciaFinanceira()
            : base(ExceptionsResource.ERR_SolicitacaoServicoComPendenciaFinanceira)
        {
        }
    }
}