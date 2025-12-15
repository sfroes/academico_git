using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaCanceladaPlanoEstudoAlunoException : SMCApplicationException
    {
        public TurmaCanceladaPlanoEstudoAlunoException(string alunos)
            : base(string.Format(ExceptionsResource.ERR_TurmaCanceladaPlanoEstudoAlunoException, alunos))
        {           
        }
    }
}
