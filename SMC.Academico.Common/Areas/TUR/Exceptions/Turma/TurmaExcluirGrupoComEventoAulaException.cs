using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions.Turma
{
    public class TurmaExcluirGrupoComEventoAulaException : SMCApplicationException
    {
        public TurmaExcluirGrupoComEventoAulaException()
            : base(ExceptionsResource.ERR_TurmaExcluirGrupoComEventoAulaException)
        {
        }
    }
}
