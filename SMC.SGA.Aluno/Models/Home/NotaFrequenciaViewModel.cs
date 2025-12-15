using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Models
{
    public class NotaFrequenciaViewModel : SMCViewModelBase
    {
        public long SeqTurma { get; set; }

        public string DescricaoOferta { get; set; }

        public string DescricaoAluno { get; set; }

        public long RegistroAcademico { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoTurma { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public double? Nota { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string Apuracao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public short? Faltas { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string SituacaoFinal { get; set; }

        public bool ExibirApuracao { get; set; }

        public bool ExibirNota { get; set; }

        public bool ExibirFaltas { get; set; }
    }
}