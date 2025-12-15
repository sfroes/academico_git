using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularOrganizacaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter("Seq")]
        public long SeqComponenteCurricular { get; set; }

        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public bool Ativo { get; set; }
    }
}