using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularCondicaoObrigatoriedadeViewModel : SMCViewModelBase, ISMCMappable, ISMCSeq
    {
        [SMCKey]
        [SMCRequired]
        [SMCSelect(nameof(GrupoCurricularDynamicModel.CondicoesObrigatoriedadeNivelEnsino), NameDescriptionField = nameof(DescricaoCondicaoObrigatoriedade))]
        [SMCSize(SMCSize.Grid20_24)]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCMapProperty("CondicaoObrigatoriedade.Descricao")]
        public string DescricaoCondicaoObrigatoriedade { get; set; }
    }
}