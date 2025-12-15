using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class JustificativaSolicitacaoServicoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }
        
        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string Token { get; set; }

        [SMCSelect]      
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public bool? Ativo { get; set; }
    }
}