using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularEntidadeResponsavelViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqComponenteCurricular { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public string NomeEntidade { get; set; }

        [SMCDescription]
        [SMCSelect(nameof(ComponenteCurricularDynamicModel.EntidadesResponsavel))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqEntidade { get; set; }
    }
}