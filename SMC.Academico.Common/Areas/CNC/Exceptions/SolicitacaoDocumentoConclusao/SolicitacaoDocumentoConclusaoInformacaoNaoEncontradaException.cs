using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}