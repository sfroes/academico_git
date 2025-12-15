using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SelecaoChancelaPlanoAtividadeViewModel
    {
        [SMCHidden]
        public long SeqItem { get; set; }

        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCSelect("SituacoesItens", StorageType = SMCStorageType.TempData)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        public long SeqSituacaoItemMatricula { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        public bool? LegendaPertencePlanoEstudo { get; set; }
    }
}
