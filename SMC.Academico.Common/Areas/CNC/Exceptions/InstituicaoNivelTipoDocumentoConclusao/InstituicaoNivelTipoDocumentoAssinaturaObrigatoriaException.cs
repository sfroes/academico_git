using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoAssinaturaObrigatoriaException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoAssinaturaObrigatoriaException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoAssinaturaObrigatoriaException)
        { }
    }
}
