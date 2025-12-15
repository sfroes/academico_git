using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class BotoesSolicitacaoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqServico { get; set; }

        public string TokenServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public bool SituacaoAtualSolicitacaoEncerradaOuConcluida { get; set; }

        public bool SituacaoAtualEtapaDiferenteLiberada { get; set; }

        public bool EtapaAtualIndisponivelCentralAtendimento { get; set; }

        public string OrientacaoAtendimento { get; set; }

        public bool SolicitantePossuiBloqueiosVigentes { get; set; }

        public bool SolicitacaoPossuiGrupoEscalonamentoComPeriodoNaoVigente { get; set; }

        public bool SolicitacaoComEtapaAtualCompartilhadaEUsuarioNaoAssociado { get; set; }

        public bool SolicitacaoComEtapaAtualNaoCompartilhadaEEntidadeUsuarioLogadoNaoResponsavel { get; set; }

        public bool SolicitacaoPossuiUsuarioResponsavel { get; set; }

        public bool SolicitacaoPossuiCodigoAdesao { get; set; }

        public bool UsuarioNaoPossuiAcessoARealizarAtendimento { get; set; }

        public bool SituacaoClassificacaoFinalizadaSemSucesso { get; set; }

        public bool SituacaoClassificacaoFinalizadaComSucesso { get; set; }

        public bool SituacaodDocumentacaoNaoRequerida { get; set; }

        public bool SituacaoSolicitanteNaoPermiteReabrirSolicitacao { get; set; }

        public bool EtapaAtualPossuiParametrizacaoDeSituacaoDeCancelamento { get; set; }

        public bool SolicitacaoServicoEDeMatricula { get; set; }

        public bool SituacaoAtualSolicitacaoEFimProcesso { get; set; }

        public bool NaoRealizadaAdezaoContrato { get; set; }
        public bool PrimeiraEtapaComSituacaoFinalEtapaEFinalizadaComSucesso { get; set; }

        public bool EtapaNaoDisponivelParaAplicacao { get; set; }

        public bool SolicitacaoAssociadaGrupoEscalonamento { get; set; }

        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        public bool SituacaoAtualSolicitacaoEncerradaComSucesso { get; set; }

        public bool UsuarioLogadoEResponsavelAtualPelaSolicitacao { get; set; }

        public bool ExibirComprovanteMatriculaETermoAdesao { get; set; }

        public bool SolicitacaoComMatriculaEfetivada { get; set; }

        public bool SituacaoAtualSolicitacaoEInicialEtapa { get; set; }

        public bool EtapaAtualVigente { get; set; }

        public bool EtapaAnteriorVigente { get; set; }

        public bool PrazoParaReabrirSolicitacaoExpirado { get; set; }

        public bool EtapaAtualEAnteriorAssociadasMesmaAplicacao { get; set; }

        /// <summary>
        /// Indicador que informa se permite ou não reabrir a solicitação
        /// </summary>
        public bool BotaoReabrirSolicitacaoHabilitado { get; set; }

        /// <summary>
        /// Mensagem informativa para bloquear o botão de reabrir solicitação
        /// </summary>
        public string MensagemInformativaBotaoReabrirSolicitacao { get; set; }

        public string DescricaoEtapaAtual { get; set; }
        public string DescricaoEtapaAnterior { get; set; }
        public string MensagemConfirmacaoRetornarEtapaAnterior { get; set; }

        public bool ConfigEtapaPossuiPaginaUploadDocumento { get; set; }
        public bool PermiteFinalizarSolicitacaoCRA { get; set; }
    }
}