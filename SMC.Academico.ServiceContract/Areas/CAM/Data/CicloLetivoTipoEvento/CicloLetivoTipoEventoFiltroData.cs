using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTipoEventoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public TipoParametroEvento? TipoParametroEvento { get; set; }
    }
}