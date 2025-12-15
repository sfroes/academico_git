using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TipoDocumentoAcademicoNaoLocalizadoException : SMCApplicationException
    {
        public TipoDocumentoAcademicoNaoLocalizadoException()
            : base(ExceptionsResource.ERR_TipoDocumentoAcademicoNaoLocalizadoException)
        { }
    }
}
