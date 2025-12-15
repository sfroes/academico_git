using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarTipoAgendaException : SMCApplicationException
    {
        public ImpossivelAlterarTipoAgendaException(string descricaoTipoAgenda)
            : base(string.Format(ExceptionsResource.ERR_ImpossivelAlterarTipoAgendaException, descricaoTipoAgenda))
        {
        }
    }
}