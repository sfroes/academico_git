using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosRelatorioSolicitacoesBloqueioItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public string NumeroProtocolo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string Solicitante { get; set; }

        public string SolicitanteSeqPessoaAtuacao
        {
            get
            {
                return $"{Solicitante} ({SeqPessoaAtuacao})";
            }
        }

        public string EtapaAtual { get; set; }

        public string SituacaoAtual { get; set; }

        public string EtapaSituacaoAtual
        {
            get
            {
                return $"{EtapaAtual} {Environment.NewLine}{SituacaoAtual}";
            }
        }

        public string TipoBloqueio { get; set; }

        public string MotivoBloqueio { get; set; }

        public string TipoMotivoBloqueio
        {
            get
            {
                return $"{TipoBloqueio} {Environment.NewLine}{MotivoBloqueio}";
            }
        }

        public string Referente { get; set; }

        public DateTime DataBloqueio { get; set; }

        public string DataBloqueioFormatada
        {
            get
            {
                return this.DataBloqueio.ToString("dd/MM/yy");
            }
        }

        public string Impede { get; set; }
    }
}
