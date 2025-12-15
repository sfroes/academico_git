using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoHierarquiaEntidadeNaoConfigurouTipoException : SMCApplicationException
    {
        public TipoHierarquiaEntidadeNaoConfigurouTipoException(string visao)
            : base(string.Format(Resources.ExceptionsResource.ERR_TipoHierarquiaEntidadeNaoConfigurouTipoException, visao))
        {

        }
    }
}
