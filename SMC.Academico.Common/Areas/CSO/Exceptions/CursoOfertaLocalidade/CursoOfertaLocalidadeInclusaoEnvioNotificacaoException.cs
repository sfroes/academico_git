using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoOfertaLocalidadeInclusaoEnvioNotificacaoException : SMCApplicationException
    {
        public CursoOfertaLocalidadeInclusaoEnvioNotificacaoException(string token)
            : base(string.Format(ExceptionsResource.ERR_CursoOfertaLocalidadeInclusaoEnvioNotificacaoException, token))
        { }
    }
}