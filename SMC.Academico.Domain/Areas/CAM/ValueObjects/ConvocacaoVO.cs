using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class ConvocacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public string Descricao { get; set; }
    }
}
