using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoComBloqueioException : SMCApplicationException
    {
        public SolicitacaoServicoComBloqueioException(string descricaoServico, string bloqueios)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoComBloqueioException, descricaoServico, bloqueios))
        {
        }
    }
}