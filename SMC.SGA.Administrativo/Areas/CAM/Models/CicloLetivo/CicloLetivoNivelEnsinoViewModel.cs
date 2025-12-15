using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoNivelEnsinoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCSelect(nameof(CicloLetivoDynamicModel.NiveisEnsinoDataSource), AutoSelectSingleItem = true, SortBy = SMCSortBy.Description)]
        [SMCDependency(nameof(CicloLetivoDynamicModel.SeqRegimeLetivo), nameof(CicloLetivoController.BuscarNiveisEnsinoDoRegimeSelect), "CicloLetivo", true)]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCMapProperty(nameof(CicloLetivoNivelEnsinoData.Seq))]
        public long SeqItem { get; set; }
    }
}