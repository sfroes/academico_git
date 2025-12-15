using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoTipoDocumentoNaoEncontradoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoTipoDocumentoNaoEncontradoException(string tokenTipoDocumento)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoTipoDocumentoNaoEncontradoException, tokenTipoDocumento))
        { }
    }
}