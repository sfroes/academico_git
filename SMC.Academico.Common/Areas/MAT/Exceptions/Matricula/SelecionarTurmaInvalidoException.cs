using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class SelecionarTurmaInvalidoException : SMCApplicationException
    {
        public SelecionarTurmaInvalidoException()
            : base(ExceptionsResource.ERR_SelecionarTurmaInvalidoException)
        {
        }
    }
}
