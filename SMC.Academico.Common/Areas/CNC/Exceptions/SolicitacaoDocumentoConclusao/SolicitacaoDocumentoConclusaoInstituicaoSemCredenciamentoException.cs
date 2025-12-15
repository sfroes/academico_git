using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao
{
    public class SolicitacaoDocumentoConclusaoInstituicaoSemCredenciamentoException : SMCApplicationException
    {
        public SolicitacaoDocumentoConclusaoInstituicaoSemCredenciamentoException()
            : base(ExceptionsResource.ERR_SolicitacaoDocumentoConclusaoInstituicaoSemCredenciamentoException)
        { }
    }
}