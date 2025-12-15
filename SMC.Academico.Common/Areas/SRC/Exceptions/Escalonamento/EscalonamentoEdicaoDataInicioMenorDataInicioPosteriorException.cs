using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoEdicaoDataInicioMenorDataInicioPosteriorException : SMCApplicationException
    {
        public EscalonamentoEdicaoDataInicioMenorDataInicioPosteriorException(string grupos)
            : base(string.Format(ExceptionsResource.ERR_EscalonamentoEdicaoDataInicioMenorDataInicioPosteriorException, grupos))
        {
        }
    }
}