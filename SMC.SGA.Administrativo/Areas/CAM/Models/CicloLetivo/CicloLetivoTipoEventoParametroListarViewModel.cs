using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoTipoEventoParametroListarViewModel : SMCViewModelBase, ISMCMappable
    {
        public TipoParametroEvento TipoParametroEvento { get; set; }
    }
}