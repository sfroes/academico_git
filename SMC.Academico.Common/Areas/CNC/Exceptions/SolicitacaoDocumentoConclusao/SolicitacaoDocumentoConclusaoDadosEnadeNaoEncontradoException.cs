using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoDadosEnadeNaoEncontradoException)
        { }
    }
}