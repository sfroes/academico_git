using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaDuplicadaException : SMCApplicationException
    {
        public TurmaDuplicadaException(string codigoFormatadoTurma)
            : base(string.Format(ExceptionsResource.ERR_TurmaDuplicadaException, codigoFormatadoTurma))
        {
        }
    }
}
