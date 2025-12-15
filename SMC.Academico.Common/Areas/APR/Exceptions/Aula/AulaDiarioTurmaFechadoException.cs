using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions.Aula
{
    public class AulaDiarioTurmaFechadoException : SMCApplicationException
    {
        public AulaDiarioTurmaFechadoException()
            : base(ExceptionsResource.ERR_AulaDiarioTurmaFechadoException)
        {
        }
    }
}