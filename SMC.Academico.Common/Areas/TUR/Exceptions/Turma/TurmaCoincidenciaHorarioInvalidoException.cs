using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaCoincidenciaHorarioInvalidoException : SMCApplicationException
    {
        public TurmaCoincidenciaHorarioInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaCoincidenciaHorarioInvalidoException, registros))
        {
        }
    }
}
