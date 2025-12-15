using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class InstituicaoEnsinoInformacaoNaoEncontradaException : SMCApplicationException
    {
        public InstituicaoEnsinoInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoEnsinoInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}
