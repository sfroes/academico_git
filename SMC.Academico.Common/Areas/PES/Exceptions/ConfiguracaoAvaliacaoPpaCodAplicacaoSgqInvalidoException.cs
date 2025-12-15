using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ConfiguracaoAvaliacaoPpaCodAplicacaoSgqInvalidoException : SMCApplicationException
    {
        public ConfiguracaoAvaliacaoPpaCodAplicacaoSgqInvalidoException()
            : base(ExceptionsResource.ERR_ConfiguracaoAvaliacaoPpaCodAplicacaoSgqInvalidoException)
        { }
    }
}