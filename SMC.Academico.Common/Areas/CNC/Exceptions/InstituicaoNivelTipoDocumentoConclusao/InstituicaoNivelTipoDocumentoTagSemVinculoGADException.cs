using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoTagSemVinculoGADException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoTagSemVinculoGADException(string tagsNaoEncontradas)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoTagSemVinculoGADException, tagsNaoEncontradas))
        { }
    }
}


