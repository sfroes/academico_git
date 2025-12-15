using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExisteProcessoServicoCicloLetivoException : SMCApplicationException
    {
        public ExisteProcessoServicoCicloLetivoException(string cicloLetivo, string entidades)
            : base(string.Format(ExceptionsResource.ERR_ExisteProcessoServicoCicloLetivo, cicloLetivo, entidades))
        {
        }
    }
}