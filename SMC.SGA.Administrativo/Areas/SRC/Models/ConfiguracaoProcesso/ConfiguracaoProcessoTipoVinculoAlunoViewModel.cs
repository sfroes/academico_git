using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoProcessoTipoVinculoAlunoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoProcesso { get; set; }
       
        [SMCRequired]
        [SMCSelect(nameof(ConfiguracaoProcessoViewModel.TiposVinculoDataSource))]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public long SeqTipoVinculoAluno { get; set; }
    }
}