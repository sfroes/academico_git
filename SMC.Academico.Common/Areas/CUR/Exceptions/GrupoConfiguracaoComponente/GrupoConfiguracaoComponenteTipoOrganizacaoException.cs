using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoComponenteTipoOrganizacaoException : SMCApplicationException
    {
        public GrupoConfiguracaoComponenteTipoOrganizacaoException()
            : base(ExceptionsResource.ERR_GrupoConfiguracaoComponenteTipoOrganizacaoException)
        {
        }
    }
}