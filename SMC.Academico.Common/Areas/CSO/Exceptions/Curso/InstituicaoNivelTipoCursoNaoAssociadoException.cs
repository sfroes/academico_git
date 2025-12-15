using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class InstituicaoNivelTipoCursoNaoAssociadoException : SMCApplicationException
    {
        public InstituicaoNivelTipoCursoNaoAssociadoException(string nivelEnsino)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelTipoCursoNaoAssociadoExceptionException, nivelEnsino))
        {
        }
    }
}