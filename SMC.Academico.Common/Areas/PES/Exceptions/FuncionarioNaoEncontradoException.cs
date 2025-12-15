using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FuncionarioNaoEncontradoException : SMCApplicationException
    {
        public FuncionarioNaoEncontradoException()
            : base(ExceptionsResource.ERR_FuncionarioNaoEncontradoException)
        {
        }
    }
}
