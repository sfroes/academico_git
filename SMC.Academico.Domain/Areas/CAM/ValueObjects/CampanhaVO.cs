using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqEntidadeResponsavel { get; set; }
    }
}