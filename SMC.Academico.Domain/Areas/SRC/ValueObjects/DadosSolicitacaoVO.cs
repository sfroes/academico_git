using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosSolicitacaoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public SolicitanteVO Solicitante { get; set; }

        public SolicitacaoVO Solicitacao { get; set; }

        public SolicitacaoOriginalVO SolicitacaoOriginal { get; set; }

        public SolicitacaoAtualizadaVO SolicitacaoAtualizada { get; set; }

        public List<HistoricoSolicitacaoVO> HistoricosSolicitacao { get; set; }

        public List<NotificacaoSolicitacaoVO> NotificacoesSolicitacao { get; set; }

        public List<DocumentoConclusaoSolicitacaoVO> DocumentosConclusaoSolicitacao { get; set; }

        public List<DocumentoSolicitacaoVO> DocumentosSolicitacao { get; set; }

        public List<TaxasSolicitacaoVO> TaxasSolicitacao { get; set; }

        public List<TitulosSolicitacaoVO> TitulosSolicitacao { get; set; }

        public List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaVO> RestricoesSolicitacoesSimultaneas { get; set; }

        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        public bool SolicitacaoPossuiTaxas { get; set; }
    }
}