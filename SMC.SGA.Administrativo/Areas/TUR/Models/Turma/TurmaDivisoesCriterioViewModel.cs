using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaDivisoesCriterioViewModel : SMCViewModelBase
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

        public TurmaOrigemAvaliacaoViewModel OrigemAvaliacao { get; set; }
    }
}