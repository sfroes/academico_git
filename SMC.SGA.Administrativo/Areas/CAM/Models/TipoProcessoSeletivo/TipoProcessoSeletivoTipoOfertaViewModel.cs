using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoProcessoSeletivoTipoOfertaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        [SMCMapProperty(nameof(TipoProcessoSeletivoTipoOfertaData.SeqTipoOferta))]
        [SMCSelect(nameof(TipoProcessoSeletivoDynamicModel.TiposOfertaDataSource), AutoSelectSingleItem = true, SortBy = SMCSortBy.Description)]
        public long SeqTipoOferta { get; set; }
    }
}