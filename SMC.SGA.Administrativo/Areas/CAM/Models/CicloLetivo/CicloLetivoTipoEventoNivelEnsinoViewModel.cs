using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoTipoEventoNivelEnsinoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCSelect(nameof(CicloLetivoTipoEventoDynamicModel.ListaNiveisEnsino))]
        public long Seq { get; set; }
    }
}