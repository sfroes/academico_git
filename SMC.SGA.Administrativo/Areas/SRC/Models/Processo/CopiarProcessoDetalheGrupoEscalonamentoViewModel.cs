using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CopiarProcessoDetalheGrupoEscalonamentoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public bool? CriarGrupo { get; set; }

        //[SMCSelect]
        //[SMCRequired]
        //[SMCConditionalReadonly(nameof(CriarGrupo), false, PersistentValue = false)]
        //[SMCDependency(nameof(CriarGrupo), nameof(ProcessoController.PreencherCampoCopiarNotificacoes), "Processo", false)]
        //[SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCHidden]
        public bool? CopiarNotificacoes { get; set; }
    }
}