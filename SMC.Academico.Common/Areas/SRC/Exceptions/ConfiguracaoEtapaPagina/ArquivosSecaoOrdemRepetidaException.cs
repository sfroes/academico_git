using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ArquivosSecaoOrdemRepetidaException : SMCApplicationException
    {
        public ArquivosSecaoOrdemRepetidaException()
          : base(ExceptionsResource.ERR_ArquivosSecaoOrdemRepetidaException)
        {
        }
    }
}
