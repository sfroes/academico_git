using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularConfiguracaoComponenteDivisaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDivisaoMatrizCurricular { get; set; }

        public string DescricaoDivisaoMatrizCurricular { get; set; }

        public short? QuantidadeItens { get; set; }

        public short? QuantidadeHoraAula { get; set; }

        public short? QuantidadeHoraRelogio { get; set; }

        public short? QuantidadeCreditos { get; set; }
    }
}
