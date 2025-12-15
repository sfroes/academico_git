using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularConfiguracaoComponenteDivisaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoMatrizCurricular { get; set; }

        [SMCMapProperty("DivisaoMatrizCurricular.DivisaoCurricularItem.Descricao")]
        public string DescricaoDivisaoMatrizCurricular { get; set; }

        public short? QuantidadeItens { get; set; }

        public short? QuantidadeHoraAula { get; set; }

        public short? QuantidadeHoraRelogio { get; set; }

        public short? QuantidadeCreditos { get; set; }
    }
}
