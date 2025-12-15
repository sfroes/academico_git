using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaSelecaoObrigatoriaException : SMCApplicationException
    {
        public TurmaSelecaoObrigatoriaException()
            : base(ExceptionsResource.ERR_TurmaSelecaoObrigatoriaException)
        {
        }
    }
}
