using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoAvaliacaoData : ISMCMappable
    {
        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        #region CamposConsulta

        public bool ApuracaoFrequencia { get; set; }

        public bool ApuracaoNota { get; set; }

        public bool? OrigemAvaliacaoTurma { get; set; }

        public bool ResponsavelTurma { get; set; }

        public bool DiarioFechado { get; set; }

        public string DescricaoOrigemAvaliacao { get; set; }

        public bool PermiteAlunoSemNota { get; set; }

        public bool PermiteMateriaLecionada { get; set; }

        public bool MateriaLecionadaObrigatoria { get; set; }

        public bool MateriaLecionadaCadastrada { get; set; }

        public List<AplicacaoAvaliacaoData> AplicacaoAvaliacoes { get; set; }

        public List<LancamentoAvaliacaoAlunosData> Alunos { get; set; }

        public List<string> DescricoesDivisaoTurma { get; set; }

        #endregion

        #region CamposRetorno
        public List<LancamentoAvaliacaoAlunoApuracaoData> Apuracoes { get; set; }

        public List<long> SeqsApuracaoExculida { get; set; } 
        #endregion
    }
}
