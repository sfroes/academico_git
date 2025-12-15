using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool PermiteCreditoComponenteCurricular { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        public short QuantidadeMinutosMinimoAula { get; set; }

        public short QuantidadeMinutosMaximoAula { get; set; }
    }
}
