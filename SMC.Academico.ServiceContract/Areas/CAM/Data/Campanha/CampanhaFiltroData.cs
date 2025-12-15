using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [SMCKeyModel]
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }
    }
}