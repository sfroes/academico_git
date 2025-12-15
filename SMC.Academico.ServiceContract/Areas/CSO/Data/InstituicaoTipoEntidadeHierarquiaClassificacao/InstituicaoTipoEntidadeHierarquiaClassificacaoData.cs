using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoTipoEntidadeHierarquiaClassificacao
{
    public class InstituicaoTipoEntidadeHierarquiaClassificacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEntidade { get; set; }

        public long SeqHierarquiaClassificacao { get; set; }

        public long? SeqTipoClassificacao { get; set; }

        public short? QuantidadeMinima { get; set; }

        public short? QuantidadeMaxima { get; set; }

        [SMCMapProperty("HierarquiaClassificacao")]
        public HierarquiaClassificacaoData HierarquiaClassificacaoData { get; set; }
    }
}
