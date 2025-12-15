using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class TaxasSolicitacaoViewModel : SMCViewModelBase
    {
        public int SeqTaxaGra { get; set; }

        [SMCCssClass("smc-size-md-21 smc-size-xs-21 smc-size-sm-21 smc-size-lg-21")]
        public string DescricaoTaxa { get; set; }

        public int SeqTituloGra { get; set; }

        public long SeqServico { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        #region Habilitar/Desabilitar botões

        [SMCHidden]
        public bool HabilitarBotaoEmitirBoleto { get; set; }

        [SMCHidden]
        public string MensagemBotaoEmitirBoleto { get; set; }

        #endregion
    }
}