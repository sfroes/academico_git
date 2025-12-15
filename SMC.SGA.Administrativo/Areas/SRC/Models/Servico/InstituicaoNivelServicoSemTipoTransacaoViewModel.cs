using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class InstituicaoNivelServicoSemTipoTransacaoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.NiveisEnsino))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long SeqNivelEnsino { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TiposVinculo), NameDescriptionField = nameof(DescricaoTipoVinculoAluno))]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(ServicoController.BuscarTiposVinculoPorNivelEnsinoSelect), "Servico", true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]      
        public long SeqTipoVinculoAluno { get; set; }

        [SMCHidden]
        public string DescricaoTipoVinculoAluno { get; set; }        
    }
}