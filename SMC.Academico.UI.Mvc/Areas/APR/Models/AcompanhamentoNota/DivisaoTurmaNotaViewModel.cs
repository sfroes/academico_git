using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class DivisaoTurmaNotaViewModel : SMCViewModelBase, ISMCMappable
    {
        public string DescricaoTurma { get; set; }
        public string CodificacaoDescricaoTurma { get; set; }
        public string Professores { get; set; }
        public string DescricaoCicloLetivo { get; set; }
        public short TotalPontos { get; set; }
        public bool ExibirFaltas { get; set; }
        public List<AvaliacoesCabecalhoViewModel> Avaliacoes { get; set; }
        public List<AlunosNotaViewModel> Alunos { get; set; } 
    }
}