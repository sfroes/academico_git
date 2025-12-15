using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoComponenteGrupoException : SMCApplicationException
    {
        public GrupoConfiguracaoComponenteGrupoException()
            : base(ExceptionsResource.ERR_GrupoConfiguracaoComponenteGrupoException)
        {
        }
    }
}