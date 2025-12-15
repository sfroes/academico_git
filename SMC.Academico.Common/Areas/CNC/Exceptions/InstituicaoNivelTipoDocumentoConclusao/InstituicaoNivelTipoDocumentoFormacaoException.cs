using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoFormacaoException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoFormacaoException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoFormacaoException)
        { }
    }
}


