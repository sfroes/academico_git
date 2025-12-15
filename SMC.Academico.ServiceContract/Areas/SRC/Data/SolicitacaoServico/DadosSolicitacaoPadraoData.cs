using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosSolicitacaoPadraoData : ISMCMappable
    {
        public long SeqServico { get; set; }

        public bool ExigeJustificativa { get; set; }

        public long? SeqJustificativa { get; set; }

        public string ObservacoesJustificativa { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapaAtual { get; set; }
    }
}