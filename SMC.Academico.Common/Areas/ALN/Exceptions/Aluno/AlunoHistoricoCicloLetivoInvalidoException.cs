using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlunoHistoricoCicloLetivoInvalidoException : SMCApplicationException
    {
        public AlunoHistoricoCicloLetivoInvalidoException()
            : base(ExceptionsResource.ERR_AlunoHistoricoCicloLetivoInvalidoException)
        { }
    }
}