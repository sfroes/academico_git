using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions.Aula
{
    public class AulaSeqDivisaoTurmaNaoInformadoException : SMCApplicationException
    {
        public AulaSeqDivisaoTurmaNaoInformadoException()
            : base(ExceptionsResource.ERR_AulaSeqDivisaoTurmaNaoInformadoException)
        {
        }
    }
}