using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoSemAlunoHistoricoException : SMCApplicationException
    {
        public SolicitacaoServicoSemAlunoHistoricoException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoSemAlunoHistoricoException)
        { }
    }
}