using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarTipoParametroException : SMCApplicationException
    {
        public ImpossivelAlterarTipoParametroException(string descricaoTipoParametro)
            : base(string.Format(ExceptionsResource.ERR_ImpossivelAlterarTipoParametroException, descricaoTipoParametro))
        {
        }
    }
}