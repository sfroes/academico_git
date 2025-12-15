using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoComponenteCurriculoException : SMCApplicationException
    {
        public GrupoConfiguracaoComponenteCurriculoException()
            : base(ExceptionsResource.ERR_GrupoConfiguracaoComponenteCurriculoException)
        {
        }
    }
}