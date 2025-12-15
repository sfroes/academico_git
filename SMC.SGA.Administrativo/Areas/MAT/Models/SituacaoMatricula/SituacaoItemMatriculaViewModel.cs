using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class SituacaoItemMatriculaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid9_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24)]
        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? SituacaoInicial { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? SituacaoFinal { get; set; }
    }
}