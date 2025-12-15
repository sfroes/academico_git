using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ConfiguracaoProcessoNaoEncontradaException : SMCApplicationException
    {
        public ConfiguracaoProcessoNaoEncontradaException(string nomeAluno)
            : base(string.Format(ExceptionsResource.ERR_ConfiguracaoProcessoNaoEncontradaException, nomeAluno))
        { }
    }
}