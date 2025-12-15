using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoProcessoSeletivoGpiViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        [SMCMapProperty(nameof(TipoProcessoSeletivoGpiData.SeqTipoProcessoGpi))]
        [SMCSelect(nameof(TipoProcessoSeletivoDynamicModel.TiposProcessoSeletivoGPIDataSource), AutoSelectSingleItem = true, SortBy = SMCSortBy.Description)]
        public long SeqTipoProcessoGpi { get; set; }
    }
}