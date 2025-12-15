using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RecuperarPaginaEtapaItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCHideLabel]
        [SMCCssClass("smc-sgaaluno-termoentregadocumentacao")]
        public bool Selected { get; set; }

        [SMCDisplay]
        //[SMCHideLabel]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid18_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        //[SMCHideLabel]
        //[SMCDisplay]
        //[SMCSize(SMCSize.Grid7_24)]
        //public string LabelConfigDocumento { get { return "Configuração de Documento"; } }

        //[SMCHideLabel]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid18_24, SMCSize.Grid18_24, SMCSize.Grid8_24)]
        //[SMCDependency(nameof(Selected), false)]
        [SMCConditionalReadonly(nameof(Selected), SMCConditionalOperation.Equals, false)]
        public ConfiguracaoDocumento? ConfiguracaoDocumento { get; set; }
    }
}