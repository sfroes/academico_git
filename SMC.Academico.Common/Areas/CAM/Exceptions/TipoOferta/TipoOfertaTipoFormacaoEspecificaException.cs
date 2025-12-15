using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class TipoOfertaTipoFormacaoEspecificaException : SMCApplicationException
    {
        public TipoOfertaTipoFormacaoEspecificaException() : base(ExceptionsResource.ERR_TipoOfertaTipoFormacaoEspecificaException)
        {
        }
    }
}