using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularBeneficioViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCRequired]
        [SMCSelect(nameof(GrupoCurricularDynamicModel.BeneficiosNivelEnsino), NameDescriptionField = nameof(DescricaoBeneficio))]
        [SMCSize(SMCSize.Grid20_24)]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCMapProperty("Beneficio.Descricao")]
        public string DescricaoBeneficio { get; set; }
    }
}