using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class ObrigatorioMesmaAgendaAlteracaoException : SMCApplicationException
    {
        public ObrigatorioMesmaAgendaAlteracaoException(string descricaoTipoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ObrigatorioMesmaAgendaAlteracao, descricaoTipoAgenda))
        {
        }
    }
}