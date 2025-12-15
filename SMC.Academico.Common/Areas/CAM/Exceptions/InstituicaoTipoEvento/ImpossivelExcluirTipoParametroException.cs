using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelExcluirTipoParametroException : SMCApplicationException
    {
        public ImpossivelExcluirTipoParametroException(string descricaoTipoParametro)
            : base(string.Format(ExceptionsResource.ERR_ImpossivelExcluirTipoParametroException, descricaoTipoParametro))
        {
        }
    }
}