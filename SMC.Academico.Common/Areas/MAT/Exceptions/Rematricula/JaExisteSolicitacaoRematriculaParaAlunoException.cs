using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class JaExisteSolicitacaoRematriculaParaAlunoException : SMCApplicationException
    {
        public JaExisteSolicitacaoRematriculaParaAlunoException(string nomeAluno)
            : base (string.Format(ExceptionsResource.ERR_JaExisteSolicitacaoRematriculaParaAlunoException, nomeAluno))
        { }
    }
}