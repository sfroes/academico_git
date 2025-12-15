using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico
{
    public class SolicitacaoServicoNaoEncontrouSituacaoMatriculaAtualException : SMCApplicationException
    {
        public SolicitacaoServicoNaoEncontrouSituacaoMatriculaAtualException()
           : base(ExceptionsResource.ERR_SolicitacaoServicoNaoEncontrouSituacaoMatriculaAtualException)
        { }
    }
}
