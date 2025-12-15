using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PrazoConclusaoInvalidoException : SMCApplicationException
    {
        public PrazoConclusaoInvalidoException()
            :base(ExceptionsResource.ERR_PrazoConclusaoInvalidoException)
        { }
    }
}