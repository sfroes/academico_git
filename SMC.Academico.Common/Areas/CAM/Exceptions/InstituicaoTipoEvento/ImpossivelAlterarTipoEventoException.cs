using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarTipoEventoException : SMCApplicationException
    {
        public ImpossivelAlterarTipoEventoException(string descricaoTipoEvento)
            : base(string.Format(ExceptionsResource.ERR_ImpossivelAlterarTipoEventoException, descricaoTipoEvento))
        {
        }
    }
}