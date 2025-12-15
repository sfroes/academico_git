using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaPreCoRequisitoInvalidoException : SMCApplicationException
    {
        public TurmaPreCoRequisitoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaPreCoRequisitoInvalidoException, registros))
        {
        }
    }
}