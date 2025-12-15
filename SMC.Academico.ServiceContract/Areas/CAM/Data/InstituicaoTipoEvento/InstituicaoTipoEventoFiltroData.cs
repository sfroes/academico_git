using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class InstituicaoTipoEventoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool? Ativo { get; set; }

        public AbrangenciaEvento? AbrangenciaEvento { get; set; }
    }
}