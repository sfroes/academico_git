using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DadosSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        public SolicitanteViewModel Solicitante { get; set; }

        public SolicitacaoViewModel Solicitacao { get; set; }

        public SolicitacaoOriginalViewModel SolicitacaoOriginal { get; set; }

        public SolicitacaoAtualizadaViewModel SolicitacaoAtualizada { get; set; }

        public List<HistoricoSolicitacaoViewModel> HistoricosSolicitacao { get; set; }

        public SMCPagerModel<NotificacaoSolicitacaoViewModel> NotificacoesSolicitacao { get; set; }

        public List<DocumentoConclusaoSolicitacaoViewModel> DocumentosConclusaoSolicitacao { get; set; }

        public List<DocumentoSolicitacaoViewModel> DocumentosSolicitacao { get; set; }

        public List<TaxaSolicitacaoViewModel> TaxasSolicitacao { get; set; }

        public List<TituloSolicitacaoViewModel> TitulosSolicitacao { get; set; }

        public List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaViewModel> RestricoesSolicitacoesSimultaneas { get; set; }

        [SMCHidden]
        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        [SMCHidden]
        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        [SMCHidden]
        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        [SMCHidden]
        public bool SolicitacaoPossuiTaxas { get; set; }

        [SMCHidden]
        public bool ExibirBotoesSolicitacao { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }
    }
}