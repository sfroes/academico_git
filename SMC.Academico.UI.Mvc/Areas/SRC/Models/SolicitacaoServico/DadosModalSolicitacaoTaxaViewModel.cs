using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoTaxaViewModel
    {
        public int SeqTaxaGra { get; set; }

        [SMCCssClass("smc-size-md-21 smc-size-xs-21 smc-size-sm-21 smc-size-lg-21")]
        public string DescricaoTaxa { get; set; }

        public int SeqTituloGra { get; set; }

        public long SeqServico { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public string NumeroProtocolo { get; set; }

        public string BackUrl { get; set; }

        #region Habilitar/Desabilitar botões

        [SMCHidden]
        public bool HabilitarBotaoEmitirBoleto { get; set; }

        [SMCHidden]
        public string MensagemBotaoEmitirBoleto { get; set; }

        #endregion
    }
}
