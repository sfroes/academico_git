using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class EventoAulaTurmaSemTabelaHorarioException : SMCApplicationException
    {
        public EventoAulaTurmaSemTabelaHorarioException() :
           base(ExceptionsResource.ERR_EventoAulaTurmaSemTabelaHorarioException)
        {
        }
    }
}
