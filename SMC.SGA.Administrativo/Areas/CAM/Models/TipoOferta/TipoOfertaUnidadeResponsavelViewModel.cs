using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoOfertaUnidadeResponsavelViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24)]
        [SMCMapProperty(nameof(TipoOfertaUnidadeResponsavelData.SeqEntidadeResponsavel))]
        [SMCSelect(nameof(TipoOfertaDynamicModel.UnidadeResponsavelDataSource), AutoSelectSingleItem = true, SortBy = SMCSortBy.Description)]
        public long SeqEntidadeResponsavel { get; set; }
    }
}