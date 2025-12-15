using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class AtoNormativoEntidadeInformacaoNaoEncontradaException : SMCApplicationException
    {
        public AtoNormativoEntidadeInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_AtoNormativoEntidadeInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}
