using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaCanceladaNotaLancadaException : SMCApplicationException
    {
        public TurmaCanceladaNotaLancadaException()
            : base(ExceptionsResource.ERR_TurmaCanceladaNotaLancadaException)
        {
        }
    }
}
