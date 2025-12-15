using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoProcessoSeletivoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelFormaIngresso { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]  
        [SMCSelect(nameof(InstituicaoNivelTipoVinculoAlunoDynamicModel.TiposProcessoSeletivoSelect), NameDescriptionField =nameof(TipoProcessoSeletivoDescricao))]
        public long SeqTipoProcessoSeletivo { get; set; }

        [SMCHidden]
        public string TipoProcessoSeletivoDescricao { get; set; }
    }
} 