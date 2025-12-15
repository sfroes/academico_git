using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoComBloqueioFimEtapaException : SMCApplicationException
    {
        public SolicitacaoServicoComBloqueioFimEtapaException(string acao)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoComBloqueioFimEtapaException, acao))
        { }
    }
}