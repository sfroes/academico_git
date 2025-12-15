using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoFrequenciaVO : ISMCMappable
    {
        public long SeqOrigemAvaliacao { get; set; }
        public string DescricaoOrigemAvaliacao { get; set; }
        public DateTime DataLimite { get; set; }
        public short QuantidadeMinutosPrazoAlteracaoFrequencia { get; set; }
        public short QuantidadeDiasPrazoApuracaoFrequencia { get; set; }
        public short CargaHoraria { get; set; }
        public List<LancamentoFrequenciaAulaVO> Aulas { get; set; }
        public List<LancamentoFrequenciaAlunoVO> Alunos { get; set; }
    }
}
