using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException : SMCApplicationException
    {
        public ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException)
        {
        }
    }
}