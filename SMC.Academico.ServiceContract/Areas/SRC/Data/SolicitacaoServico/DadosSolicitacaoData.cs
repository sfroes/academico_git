using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosSolicitacaoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public SolicitanteData Solicitante { get; set; }

        public SolicitacaoData Solicitacao { get; set; }

        public SolicitacaoOriginalData SolicitacaoOriginal { get; set; }

        public SolicitacaoAtualizadaData SolicitacaoAtualizada { get; set; }

        public List<HistoricoSolicitacaoData> HistoricosSolicitacao { get; set; }

        public List<NotificacaoSolicitacaoData> NotificacoesSolicitacao { get; set; }

        public List<DocumentoConclusaoSolicitacaoData> DocumentosConclusaoSolicitacao { get; set; }

        public List<DocumentoSolicitacaoData> DocumentosSolicitacao { get; set; }

        public List<TaxasSolicitacaoData> TaxasSolicitacao { get; set; }

        public List<TitulosSolicitacaoData> TitulosSolicitacao { get; set; }

        public List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaData> RestricoesSolicitacoesSimultaneas { get; set; }

        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        public bool SolicitacaoPossuiTaxas { get; set; }
    }
}