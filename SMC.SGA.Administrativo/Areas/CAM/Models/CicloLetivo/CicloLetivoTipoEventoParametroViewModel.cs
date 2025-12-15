using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoTipoEventoParametroViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCSize(SMCSize.Grid22_24)]
        [SMCSelect(nameof(CicloLetivoTipoEventoDynamicModel.ListaParametros))]
        public long SeqInstituicaoTipoEventoParametro { get; set; }
    }
}