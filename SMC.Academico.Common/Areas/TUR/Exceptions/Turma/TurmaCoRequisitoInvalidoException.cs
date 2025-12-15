using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaCoRequisitoInvalidoException : SMCApplicationException
    {
        public TurmaCoRequisitoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaCoRequisitoInvalidoException, registros))
        {           
        }
    }
}
