using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoNenhumTipoVinculoException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoNenhumTipoVinculoException(string tags)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoNenhumTipoVinculoException, tags))
        { }
    }
}


