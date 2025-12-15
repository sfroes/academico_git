using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class TermoAdesaoNaoEncontradoException : SMCApplicationException
    {
        public TermoAdesaoNaoEncontradoException(string nomeAluno)
            : base(string.Format(ExceptionsResource.ERR_TermoAdesaoNaoEncontradoException, nomeAluno))
        { }
    }
}