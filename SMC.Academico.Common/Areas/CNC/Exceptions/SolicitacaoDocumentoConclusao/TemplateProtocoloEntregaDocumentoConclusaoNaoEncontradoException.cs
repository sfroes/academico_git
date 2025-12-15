using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class TemplateProtocoloEntregaDocumentoConclusaoNaoEncontradoException : SMCApplicationException
    {
        public TemplateProtocoloEntregaDocumentoConclusaoNaoEncontradoException()
            : base(ExceptionsResource.ERR_TemplateProtocoloEntregaDocumentoConclusaoNaoEncontradoException)
        { }
    }
}