using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTipoEventoParametroVO : ISMCMappable
    {
        public long SeqInstituicaoTipoEventoParametro { get; set; }

        [SMCMapProperty("InstituicaoTipoEventoParametro.TipoParametroEvento")]
        public TipoParametroEvento TipoParametroEvento { get; set; }
    }
}