using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoHierarquiaEntidadeSemArvoreException : SMCApplicationException
    {
        public TipoHierarquiaEntidadeSemArvoreException(string visao)
            : base(string.Format(Resources.ExceptionsResource.ERR_TipoHierarquiaEntidadeSemArvoreException, visao))
        { }
    }
}