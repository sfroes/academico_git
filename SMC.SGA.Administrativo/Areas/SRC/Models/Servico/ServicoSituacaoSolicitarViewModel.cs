using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoSituacaoSolicitarViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.Situacoes))]       
        [SMCSize(SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24)]
        public long SeqSituacao { get; set; }

        [SMCHidden]
        public PermissaoServico PermissaoServico
        {
            get
            {
                return PermissaoServico.CriarSolicitacao;
            }
        }
    }
}