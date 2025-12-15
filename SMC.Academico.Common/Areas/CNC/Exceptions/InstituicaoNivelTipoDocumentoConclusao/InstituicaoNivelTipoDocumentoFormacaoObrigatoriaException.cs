using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoFormacaoObrigatoriaException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoFormacaoObrigatoriaException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoFormacaoObrigatoriaException)
        { }
    }
}
