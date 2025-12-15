using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoArquivoSemTemplatePadraoException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoArquivoSemTemplatePadraoException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoArquivoSemTemplatePadraoException)
        { }
    }
}


