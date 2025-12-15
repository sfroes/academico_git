using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class RequisitoItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public TipoRequisitoAssociado Associado { get; set; }

        public TipoRequisito TipoRequisito { get; set; }

        public TipoRequisitoItem TipoRequisitoItem { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public OutroRequisito? OutroRequisito { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public bool? GrupoPertenceCurriculoMatrizOferta { get; set; }

        public short? QuantidadeOutroRequisito { get; set; }

        public string DescricaoRequisitoDivisao { get; set; }

        public string DescricaoRequisitoComponente { get; set; }

        public string DescricaoRequisitoOutro { get; set; }

        public string DescricaoRequisitoGrupoCurricular { get; set; }
    }
}