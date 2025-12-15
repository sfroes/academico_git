using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class ObrigatorioAgendaDistintaInclusaoException : SMCApplicationException
    {
        public ObrigatorioAgendaDistintaInclusaoException(string descricaoTipoAgenda, string descricaoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ObrigatorioAgendaDistintaInclusao, descricaoTipoAgenda, descricaoAgenda))
        {
        }
    }
}