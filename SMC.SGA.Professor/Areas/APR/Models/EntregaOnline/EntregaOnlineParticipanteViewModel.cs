using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Professor.Areas.APR.Models.EntregaOnline
{
    public class EntregaOnlineParticipanteViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid1_24)]
        [SMCLegendItemDisplay(GenerateLabel = false)]
        public ResponsavelEntregaOnline ResponsavelEntregaLegenda { get; set; }

        [SMCHidden]
        public bool ResponsavelEntrega { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        public long NumeroRA { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string NomeAluno { get; set; }

        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        [SMCMaxValue("Valor")]
        [SMCDecimalDigits(2)]
        [SMCConditionalReadonly(nameof(BloquearAlteracoes), true, PersistentValue = true)]
        public decimal? Nota { get; set; }

        [SMCConditionalReadonly(nameof(BloquearAlteracoes), true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(Nota), SMCConditionalOperation.Equals, "", null, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid9_24, SMCSize.Grid9_24, SMCSize.Grid10_24)]
        public string Comentario { get; set; }

        [SMCHidden]
        public bool BloquearAlteracoes { get; set; }

        [SMCHidden]
        public long SeqAlunoHistorico { get; set; }
    }
}