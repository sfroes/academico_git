using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataFimParcelaMenorDataFimEscalonamento : SMCApplicationException
    {
        public EscalonamentoDataFimParcelaMenorDataFimEscalonamento()
            : base(ExceptionsResource.ERR_EscalonamentoDataFimParcelaMenorDataFimEscalonamento)
        {
        }
    }
}