using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORG.Resources;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class MantenedoraInformacaoNaoEncontradaException : SMCApplicationException
    {
        public MantenedoraInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_MantenedoraInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}
