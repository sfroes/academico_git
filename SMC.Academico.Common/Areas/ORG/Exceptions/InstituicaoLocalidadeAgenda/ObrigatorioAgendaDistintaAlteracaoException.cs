using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class ObrigatorioAgendaDistintaAlteracaoException : SMCApplicationException
    {
        public ObrigatorioAgendaDistintaAlteracaoException(string descricaoTipoAgenda, string descricaoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ObrigatorioAgendaDistintaAlteracao, descricaoTipoAgenda, descricaoAgenda))
        {
        }
    }
}