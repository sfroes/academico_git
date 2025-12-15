using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException : SMCApplicationException
    {
        public EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException(string grupos)
            : base(string.Format(ExceptionsResource.ERR_EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException, grupos))
        {
        }
    }
}