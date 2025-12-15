using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoAvaliacaoAlunosData : ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }
        public long NumeroRegistroAcademico { get; set; }
        public bool Formado { get; set; }
        public string Nome { get; set; }
        public short? Faltas { get; set; }
        public SituacaoHistoricoEscolar SituacaoFinal { get; set; }
        public string DescricaoSituacaoFinal { get; set; }
        public decimal? Nota { get; set; }
        public bool AlunoAprovado { get; set; }
        public bool AlunoDispensado { get; set; }
        public List<LancamentoAvaliacaoAlunoApuracaoData> Apuracoes { get; set; }
    }
}
