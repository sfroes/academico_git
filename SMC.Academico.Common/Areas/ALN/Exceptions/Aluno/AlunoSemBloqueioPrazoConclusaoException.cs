using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlunoSemBloqueioPrazoConclusaoException : SMCApplicationException
    {
        public AlunoSemBloqueioPrazoConclusaoException()
            : base(ExceptionsResource.ERR_AlunoSemBloqueioPrazoConclusaoException)
        { }
    }
}