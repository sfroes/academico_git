using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.APR.Models.EntregaOnline
{
    public class EntregaOnlineParticipanteViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }               

        [SMCHidden]
        public bool BloquearAluno { get { return ResponsavelEntrega; } }

        [SMCConditionalReadonly(nameof(BloquearAluno), true, PersistentValue = true, RuleName = "CRS1")]
        [SMCConditionalReadonly(nameof(EntregaOnlineViewModel.HabilitarCampos), false, PersistentValue = true, RuleName = "CRS2")]
        [SMCConditionalRule("CRS1 || CRS2")]
        [SMCSelect(nameof(EntregaOnlineViewModel.Alunos))]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid17_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public long SeqAlunoHistorico { get; set; }

        [SMCConditionalReadonly(nameof(BloquearAluno), true, PersistentValue = true, RuleName = "CRR1")]
        [SMCConditionalReadonly(nameof(BloquearAluno), false, PersistentValue = true, RuleName = "CRR2")]
        [SMCConditionalRule("CRR1 || CRR2")]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid7_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public bool ResponsavelEntrega { get; set; }
    }
}