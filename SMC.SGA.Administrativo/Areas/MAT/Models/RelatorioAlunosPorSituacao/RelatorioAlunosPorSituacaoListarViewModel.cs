using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorSituacaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public TipoRelatorio TipoRelatorio { get; set; }

        [SMCHidden]
        public bool? ImprimirComponenteCurricularSemCreditos { get; set; }

        [SMCHidden]
        public bool? ExibeProfessor { get; set; }

        [SMCHidden]
        public TipoDeclaracaoDisciplinaCursada? ExibirNaDeclaracao { get; set; }

        [SMCHidden]
        public bool? ExibirEmentasComponentesCurriculares { get; set; }

        [SMCHidden]
        public List<long> SelectedValues { get; set; }

        public SMCPagerModel<RelatorioAlunosPorSituacaoListarItemViewModel> Alunos { get; set; }
    }
}