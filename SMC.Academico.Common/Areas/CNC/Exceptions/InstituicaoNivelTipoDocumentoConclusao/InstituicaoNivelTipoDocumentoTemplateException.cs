using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class InstituicaoNivelTipoDocumentoTemplateException : SMCApplicationException
    {
        public InstituicaoNivelTipoDocumentoTemplateException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoDocumentoTemplateException)
        { }
    }
}


