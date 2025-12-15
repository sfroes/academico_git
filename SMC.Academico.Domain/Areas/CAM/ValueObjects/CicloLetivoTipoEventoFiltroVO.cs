using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTipoEventoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public TipoParametroEvento? TipoParametroEvento { get; set; }
    }
}