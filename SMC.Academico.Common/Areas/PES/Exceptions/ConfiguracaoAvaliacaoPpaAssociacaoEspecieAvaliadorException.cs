using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ConfiguracaoAvaliacaoPpaAssociacaoEspecieAvaliadorException : SMCApplicationException
    {
        public ConfiguracaoAvaliacaoPpaAssociacaoEspecieAvaliadorException()
          : base(ExceptionsResource.ERR_ConfiguracaoAvaliacaoPpaAssociacaoEspecieAvaliadorException)
        { }
    }
}
