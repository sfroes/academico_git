using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class InstituicaoNivelTipoOfertaCursoNaoAssociadoException : SMCApplicationException
    {
        public InstituicaoNivelTipoOfertaCursoNaoAssociadoException(string nivelEnsino)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelTipoOfertaCursoNaoAssociadoException, nivelEnsino))
        { }
    }
}