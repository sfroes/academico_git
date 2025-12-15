using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class AlunoSemMatrizAssociadaException : SMCApplicationException
    {
        public AlunoSemMatrizAssociadaException(string nomeAluno)
            : base(string.Format(ExceptionsResource.ERR_AlunoSemMatrizAssociadaException, nomeAluno))
        { }
    }
}