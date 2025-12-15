using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularDivisaoComponenteData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDivisaoMatrizCurricularComponente { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public short QuantidadeGrupos { get; set; }

        public short? QuantidadeProfessores { get; set; }

        public short? NotaMaxima { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public bool ApurarFrequencia { get; set; }

        public bool TipoComponenteCurricularTurma { get; set; }

        public bool TipoDivisaoComponenteArtigo { get; set; }

        public ComprovacaoArtigo? ComprovacaoArtigoMinima { get; set; }

        public TipoDistribuicaoAula TipoDistribuicaoAula { get; set; }

        public TipoPulaFeriado TipoPulaFeriado { get; set; }

        public TipoPagamentoAula TipoPagamentoAula { get; set; }

        public bool? AulaSabado { get; set; }

        public bool? MateriaLecionadaObrigatoria { get; set; }

        [SMCMapProperty("DivisaoComponente.CargaHorariaGrade")]
        public int? DivisaoComponenteCargaHorariaGrade { get; set; }
    }
}