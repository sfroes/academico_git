using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoExtensaoNaoPermitidaException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoExtensaoNaoPermitidaException(string nomeArquivo)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoExtensaoNaoPermitidaException, nomeArquivo))
        { }
    }
}