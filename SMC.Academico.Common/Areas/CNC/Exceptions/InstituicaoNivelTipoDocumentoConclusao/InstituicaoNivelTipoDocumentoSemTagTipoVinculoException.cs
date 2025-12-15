using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoSemTagTipoVinculoException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoSemTagTipoVinculoException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoSemTagTipoVinculoException)
        { }
    }
}
