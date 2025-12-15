using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class ExisteEventoAgendaException : SMCApplicationException
    {
        public ExisteEventoAgendaException(string descricaoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ExisteEventoAgendaException, descricaoAgenda))
        {
        }
    }
}