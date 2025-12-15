using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class TipoOfertaTipoItemHierarquiaOfertaGpiException : SMCApplicationException
    {
        public TipoOfertaTipoItemHierarquiaOfertaGpiException() : base(ExceptionsResource.ERR_TipoOfertaTipoItemHierarquiaOfertaGpiException)
        {
        }
    }
}