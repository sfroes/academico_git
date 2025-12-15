using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioInstituicaoExternaData : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqParceriaIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        [SMCMapProperty("InstituicaoExterna.Nome")]
        public string Nome { get; set; }

        public bool? Ativo { get; set; }
    }
}