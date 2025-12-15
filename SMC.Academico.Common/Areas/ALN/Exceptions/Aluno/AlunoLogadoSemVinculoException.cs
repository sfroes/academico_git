using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlunoLogadoSemVinculoException : SMCApplicationException
    {
        public AlunoLogadoSemVinculoException()
            : base(ExceptionsResource.ERR_AlunoLogadoSemVinculoException)
        {

        }
    }
}