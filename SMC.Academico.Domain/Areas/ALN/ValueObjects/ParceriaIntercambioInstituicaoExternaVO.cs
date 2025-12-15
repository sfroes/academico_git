using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParceriaIntercambioInstituicaoExternaVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqParceriaIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        [SMCMapProperty("InstituicaoExterna.Nome")]
        public string Nome { get; set; }
    }
}