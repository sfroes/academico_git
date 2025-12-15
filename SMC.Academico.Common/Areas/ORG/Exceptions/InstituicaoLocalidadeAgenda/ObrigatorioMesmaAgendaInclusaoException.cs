using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class ObrigatorioMesmaAgendaInclusaoException : SMCApplicationException
    {
        public ObrigatorioMesmaAgendaInclusaoException(string descricaoTipoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ObrigatorioMesmaAgendaInclusao, descricaoTipoAgenda))
        {
        }
    }
}