using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class DocumentoAguardandoValidacaoException : SMCApplicationException
    {
        public DocumentoAguardandoValidacaoException()
            : base(string.Format(ExceptionsResource.ERR_Documento_Aguardando_Validacao))
        { }
    }
}
