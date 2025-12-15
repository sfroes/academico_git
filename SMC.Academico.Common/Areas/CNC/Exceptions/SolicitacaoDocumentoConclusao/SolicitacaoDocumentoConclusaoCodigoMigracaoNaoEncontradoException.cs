using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoCodigoMigracaoNaoEncontradoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoCodigoMigracaoNaoEncontradoException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoCodigoMigracaoNaoEncontradoException)
        { }
    }
}