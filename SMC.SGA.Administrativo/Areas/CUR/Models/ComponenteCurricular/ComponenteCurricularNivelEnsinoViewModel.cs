using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Views.ComponenteCurricular.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularNivelEnsinoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter("Seq")]
        public long SeqComponenteCurricular { get; set; }

        [SMCHidden]
        public bool Responsavel { get; set; }

        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        public string NivelEnsinoDescricao { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Filter | SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public string NivelEnsinoDescricaoFormatada
        {
            get { return this.Responsavel ? $"{this.NivelEnsinoDescricao} - {UIResource.Label_NivelEnsinoResponsavel}" : this.NivelEnsinoDescricao; }
        }
    }
}