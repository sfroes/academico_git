using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AssociacaoOrientadorIngressanteItemViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid10_24)]
        [SMCOrder(0)]
        [SMCSelect(nameof(AssociacaoOrientadorIngressanteViewModel.TiposParticipacaoOrientacao), NameDescriptionField = nameof(DescricaoTipoParticipacaoOrientacao))]
        public long TipoParticipacaoOrientacao { get; set; }

        [SMCHidden]
        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        [SMCOrder(1)]
        [SMCSelect(nameof(AssociacaoOrientadorIngressanteViewModel.Colaboradores), NameDescriptionField = nameof(NomeColaborador))]
        [SMCUnique]
        public long SeqColaborador { get; set; }

        [SMCHidden]
        public string NomeColaborador { get; set; }
    }
}