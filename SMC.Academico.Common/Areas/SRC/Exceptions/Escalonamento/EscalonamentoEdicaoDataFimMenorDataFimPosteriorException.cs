using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoEdicaoDataFimMenorDataFimPosteriorException : SMCApplicationException
    {
        public EscalonamentoEdicaoDataFimMenorDataFimPosteriorException(string grupos)
            : base(string.Format(ExceptionsResource.ERR_EscalonamentoEdicaoDataFimMenorDataFimPosteriorException, grupos))
        {
        }
    }
}