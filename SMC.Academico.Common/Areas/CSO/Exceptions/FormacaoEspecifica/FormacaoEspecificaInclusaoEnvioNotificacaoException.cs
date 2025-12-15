using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaInclusaoEnvioNotificacaoException : SMCApplicationException
    {
        public FormacaoEspecificaInclusaoEnvioNotificacaoException(string token)
            : base(string.Format(ExceptionsResource.ERR_FormacaoEspecificaInclusaoEnvioNotificacaoException,token))
        { }
    }
}