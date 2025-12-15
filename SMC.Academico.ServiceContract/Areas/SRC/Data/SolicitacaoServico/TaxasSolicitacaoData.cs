using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class TaxasSolicitacaoData : ISMCMappable
    {
        public int SeqTaxaGra { get; set; }

        public string DescricaoTaxa { get; set; }

        public int SeqTituloGra { get; set; }

        public long SeqServico { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public bool HabilitarBotaoEmitirBoleto { get; set; }

        public string MensagemBotaoEmitirBoleto { get; set; }
    }
}
