using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqDivisaoComponente { get; set; }
        public long? SeqHistoricoConfiguracaoGradeAtual { get; set; }
        public long SeqTurma { get; set; }
        public short NumeroGrupo { get; set; }
        public short QuantidadeVagas { get; set; }
        public long? SeqLocalidade { get; set; }
        public long SeqOrigemAvaliacao { get; set; }
        public short? QuantidadeVagasOcupadas { get; set; }
        public string InformacoesAdicionais { get; set; }
        public bool DivisaoTurmaPossuiConfiguracaoesGrade { get; set; }
        public OrigemAvaliacaoData OrigemAvaliacao { get; set; }
        public bool DiarioFechado { get; set; }
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }
        public bool? PermiteAvaliacaoParcial { get; set; }
        public bool? PermiteLancamentoFrequencia { get; set; }
        public bool GerarOrientacao { get; set; }
        public short? CargaHoraria { get; set; }
        public string DescricaoComponenteCurricularOrganizacao { get; set; }
        public string TipoDivisaoDescricao { get; set; }
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }
        public string TurmaCodigoFormatado { get; set; }
        public string DescricaoFormatada { get; set; }
        public string DescricaoFormatadaSemNumero { get; set; }
        public string DivisaoTurmaRelatorioDescricao { get; set; }
    }
}
