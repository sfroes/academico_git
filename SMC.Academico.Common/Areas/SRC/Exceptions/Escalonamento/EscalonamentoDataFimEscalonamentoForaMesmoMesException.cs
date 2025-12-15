using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataFimEscalonamentoForaMesmoMesException : SMCApplicationException
    {
        public EscalonamentoDataFimEscalonamentoForaMesmoMesException()
            : base(ExceptionsResource.ERR_EscalonamentoDataFimEscalonamentoForaMesmoMes)
        {
        }
    }
}