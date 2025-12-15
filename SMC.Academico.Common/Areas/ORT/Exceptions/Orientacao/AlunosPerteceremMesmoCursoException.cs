using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class AlunosPerteceremMesmoCursoException : SMCApplicationException
    {
        public AlunosPerteceremMesmoCursoException(string listaAlunos)
            : base(string.Format(ExceptionsResource.ERR_AlunosPerteceremMesmoCursoException, listaAlunos))
        {
        }
    }
}