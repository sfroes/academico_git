using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ConfiguracaoAvaliacaoPpaAlteracaoCargaJaRealizadaException : SMCApplicationException
    {
        public ConfiguracaoAvaliacaoPpaAlteracaoCargaJaRealizadaException()
           : base(ExceptionsResource.ERR_ConfiguracaoAvaliacaoPpaAlteracaoCargaJaRealizadaException)
        { }
    }
}
