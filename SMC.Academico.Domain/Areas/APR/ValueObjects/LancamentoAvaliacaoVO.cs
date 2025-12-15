using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoAvaliacaoVO : ISMCMappable
    {
        public long SeqOrigemAvaliacao { get; set; }

        public bool? ApuracaoFrequencia { get; set; }

        public bool? ApuracaoNota { get; set; }

        public bool Orientacao { get; set; }

        public bool OrigemAvaliacaoTurma { get; set; }

        public bool ResponsavelTurma { get; set; }

        public bool DiarioFechado { get; set; }

        public string DescricaoOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public bool PermiteMateriaLecionada { get; set; }

        public bool MateriaLecionadaObrigatoria { get; set; }

        public bool MateriaLecionadaCadastrada { get; set; }

        public bool PermiteAlunoSemNota { get; set; }

        public List<AplicacaoAvaliacaoVO> AplicacaoAvaliacoes { get; set; }

        public List<LancamentoAvaliacaoAlunosVO> Alunos { get; set; }

        public List<LancamentoAvaliacaoAlunoApuracaoVO> Apuracoes { get; set; }

        public List<long> SeqsApuracaoExculida { get; set; }

        public List<string> DescricoesDivisaoTurma { get; set; }
    }
}
