using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlunoMigracaoInvalidoException : SMCApplicationException
    {
        public AlunoMigracaoInvalidoException()
            : base(ExceptionsResource.ERR_AlunoMigracaoInvalidoException)
        { }
    }
}