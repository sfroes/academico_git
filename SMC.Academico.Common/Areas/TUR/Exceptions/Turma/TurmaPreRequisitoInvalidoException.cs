using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaPreRequisitoInvalidoException : SMCApplicationException
    {
        public TurmaPreRequisitoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaPreRequisitoInvalidoException, registros))
        {
        }
    }
}