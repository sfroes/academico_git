using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoArquivoInvalidoException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoArquivoInvalidoException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoArquivoInvalidoException)
        { }
    }
}


