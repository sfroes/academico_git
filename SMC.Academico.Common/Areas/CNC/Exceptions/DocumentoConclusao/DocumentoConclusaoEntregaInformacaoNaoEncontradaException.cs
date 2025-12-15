using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class DocumentoConclusaoEntregaInformacaoNaoEncontradaException : SMCApplicationException
    {
        public DocumentoConclusaoEntregaInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_DocumentoConclusaoEntregaInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}