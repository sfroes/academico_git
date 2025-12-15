using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoOfertaLocalidadeInformacaoNaoEncontradaException : SMCApplicationException
    {
        public CursoOfertaLocalidadeInformacaoNaoEncontradaException(string campoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_CursoOfertaLocalidadeInformacaoNaoEncontradaException, campoObrigatorio))
        { }
    }
}
