using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class DocumentoRequeridoCampoPermiteEntregaPosteriorNaoPodeSerAlteradoException : SMCApplicationException
    {
        public DocumentoRequeridoCampoPermiteEntregaPosteriorNaoPodeSerAlteradoException() 
            : base(ExceptionsResource.ERR_DocumentoPermiteEntregaPosteriorNaoPodeSerAlterado)
        {}
    }
}
