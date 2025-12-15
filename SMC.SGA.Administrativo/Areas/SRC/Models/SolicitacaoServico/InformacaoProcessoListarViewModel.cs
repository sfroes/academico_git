using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class InformacaoProcessoListarViewModel : SMCViewModelBase
    {
        public long SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        public bool ExibirData { get; set; }

        [SMCHidden]
        public bool ExibirPrazo { get; set; }

        public SMCPagerModel<InformacaoProcessoItemViewModel> InformacoesProcesso { get; set; }
    }
}