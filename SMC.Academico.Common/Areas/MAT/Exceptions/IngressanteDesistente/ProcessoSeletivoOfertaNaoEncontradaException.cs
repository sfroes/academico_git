using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ProcessoSeletivoOfertaNaoEncontradaException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaNaoEncontradaException()
            : base(ExceptionsResource.ERR_ProcessoSeletivoOfertaNaoEncontradaException)
        {
        }
    }
}