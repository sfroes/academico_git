using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConfigurarTagsDeclaracaoGenericaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public bool? PermiteEditarDado { get; set; }

        [SMCHidden]
        public TipoPreenchimentoTag TipoPreenchimentoTag { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCDisplay]
        [SMCCssClass("smc-sga-tags-documento-descricao")]
        public string DescricaoTag { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCCssClass("smc-sga-tags-documento-explicacao")]
        [SMCDisplay]
        public string InformacaoTag { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCCssClass("smc-sga-tags-documento-valor")]
        [SMCConditionalReadonly(nameof(PermiteEditarDado), SMCConditionalOperation.NotEqual, true, PersistentValue = true)]
        public string Valor { get; set; }
    }
}