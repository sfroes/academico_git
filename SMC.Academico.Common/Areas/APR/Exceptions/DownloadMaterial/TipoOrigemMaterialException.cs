using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class TipoOrigemMaterialException : SMCApplicationException
    {
        public TipoOrigemMaterialException()
            : base(ExceptionsResource.ERR_TipoOrigemMaterialException)
        {
        }
    }
}