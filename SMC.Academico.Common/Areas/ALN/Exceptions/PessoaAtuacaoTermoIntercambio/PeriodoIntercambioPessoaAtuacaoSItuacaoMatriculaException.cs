using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioPessoaAtuacaoSItuacaoMatriculaException : SMCApplicationException
    {
        public PeriodoIntercambioPessoaAtuacaoSItuacaoMatriculaException() : base (ExceptionsResource.ERR_PeriodoIntercambioPessoaAtuacaoSItuacaoMatriculaException)
        {}
    }
}
