using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoParametrizacaoTipoDocumentoNaoEncontradaException)
        { }
    }
}