using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class ApuracaoFrequenciaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public bool AlunoComHistorico { get; set; }

        [SMCLegendItemDisplay(GenerateLabel = false)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        [SMCOrder(0)]
        public SituacaoAlunoHistoricoEscolar Situacao { get { return AlunoComHistorico ? SituacaoAlunoHistoricoEscolar.ComHistorico : SituacaoAlunoHistoricoEscolar.SemHistorico; } }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid4_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        [SMCOrder(1)]
        public string NumeroRegistroAcademico { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid14_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
        [SMCOrder(2)]
        public string NomeAluno { get; set; }

        [SMCConditionalReadonly(nameof(AlunoFormado), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalRequired(nameof(AlunoFormado), SMCConditionalOperation.Equals, false)]
        [SMCOrder(3)]
        public int NumeroFaltas { get; set; }

        [SMCReadOnly]
        [SMCMath("{0} + {1}", 0, SMCSize.Grid2_24 , nameof(FaltasAtuais), nameof(NumeroFaltas))]
        [SMCOrder(4)]
        public int Total { get; set; }

        [SMCHidden]
        public int FaltasAtuais { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqAlunoHistoricoCicloLetivo { get; set; }

        [SMCHidden]
        public long SeqAula { get; set; }

        [SMCHidden]
        public bool AlunoFormado { get; set; }

        [SMCHidden]
        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }
    }
}