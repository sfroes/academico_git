using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoFrequenciaData : ISMCMappable
    {
        public long SeqOrigemAvaliacao { get; set; }
        public string DescricaoOrigemAvaliacao { get; set; }
        public DateTime DataLimite { get; set; }
        public short QuantidadeMinutosPrazoAlteracaoFrequencia { get; set; }
        public short QuantidadeDiasPrazoApuracaoFrequencia { get; set; }
        public short CargaHoraria { get; set; }
        public List<LancamentoFrequenciaAulaData> Aulas { get; set; }
        public List<LancamentoFrequenciaAlunoData> Alunos { get; set; }
    }
}
