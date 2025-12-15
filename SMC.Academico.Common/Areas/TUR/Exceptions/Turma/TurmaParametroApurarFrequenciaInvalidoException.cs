using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaParametroApurarFrequenciaInvalidoException : SMCApplicationException
    {
        public TurmaParametroApurarFrequenciaInvalidoException()
            : base(ExceptionsResource.ERR_TurmaParametroApurarFrequenciaInvalidoException)
        {
        }
    }
}
