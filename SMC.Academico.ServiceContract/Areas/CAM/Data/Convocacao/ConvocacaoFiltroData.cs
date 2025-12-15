using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ConvocacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [SMCMapProperty("SeqCicloLetivo.Seq")]
        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqProcessoSeletivo { get; set; }
    }
}