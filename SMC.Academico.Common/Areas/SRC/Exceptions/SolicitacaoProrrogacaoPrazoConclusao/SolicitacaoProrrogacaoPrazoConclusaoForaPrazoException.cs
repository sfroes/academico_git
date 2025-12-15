using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoProrrogacaoPrazoConclusaoForaPrazoException : SMCApplicationException
    {
        public SolicitacaoProrrogacaoPrazoConclusaoForaPrazoException(string quantidadeProrrogacao, string dataLimite)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoProrrogacaoPrazoConclusaoForaPrazoException, quantidadeProrrogacao, dataLimite))
        { }
    }
}