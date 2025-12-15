using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ALN.Resources;


namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class MaisDeUmAlunoSelecionadoException : SMCApplicationException
    {
        public MaisDeUmAlunoSelecionadoException()
              : base(ExceptionsResource.ERR_MaisDeUmAlunoSelecionadoException)
        {
        }
    }
}
