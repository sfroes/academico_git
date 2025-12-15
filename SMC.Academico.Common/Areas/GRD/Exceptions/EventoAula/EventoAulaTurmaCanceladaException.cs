using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class EventoAulaTurmaCanceladaException : SMCApplicationException
    {
        public EventoAulaTurmaCanceladaException() :
           base(ExceptionsResource.ERR_EventoAulaTurmaCanceladaException)
        {
        }
    }
}
