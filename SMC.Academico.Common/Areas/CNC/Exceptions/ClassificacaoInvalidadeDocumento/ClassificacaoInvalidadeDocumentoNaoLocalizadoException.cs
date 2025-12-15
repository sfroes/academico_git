using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class ClassificacaoInvalidadeDocumentoNaoLocalizadoException  : SMCApplicationException
    {
        public ClassificacaoInvalidadeDocumentoNaoLocalizadoException() : base(ExceptionsResource.ERR_ClassificacaoInvalidadeDocumentoNaoLocalizadoException)
        { }
    }
}
