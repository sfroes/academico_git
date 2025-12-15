using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoModeloPapelObrigatorioException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoModeloPapelObrigatorioException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoModeloPapelObrigatorioException)
        { }
    }
}
