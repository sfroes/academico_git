using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoServicoListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqSolicitacaoServicoEtapa { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqSolicitacaoMatricula { get; set; }

        public long? SeqConfiguracaoEtapa { get; set; }

        public long? SeqIngressante { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

        public bool SituacaoClassificacaoFinalizadaSemSucesso { get; set; }

        public bool SituacaoClassificacaoFinalizadaComSucesso { get; set; }

        public bool SituacaodDocumentacaoNaoRequerida { get; set; }

        public Guid? CodigoAdesao { get; set; }

        public bool ExibirChancela { get; set; }

        public string InstructionsChancela { get; set; }

        public string InstructionsEfetivacaoMatricula { get; set; }

        public bool SituacaoAtualSolicitacaoEncerradaOuConcluida { get; set; }

        public bool SituacaoAtualEtapaDiferenteLiberada { get; set; }

        public bool EtapaAtualIndisponivelCentralAtendimento { get; set; }

        public bool SolicitantePossuiBloqueiosVigentes { get; set; }

        public bool SolicitacaoPossuiGrupoEscalonamentoComPeriodoNaoVigente { get; set; }

        public bool SolicitacaoComEtapaAtualCompartilhadaEUsuarioNaoAssociado { get; set; }

        public bool SolicitacaoComEtapaAtualNaoCompartilhadaEEntidadeUsuarioLogadoNaoResponsavel { get; set; }

        public bool SolicitacaoPossuiUsuarioResponsavel { get; set; }

        public bool UsuarioNaoPossuiAcessoARealizarAtendimento { get; set; }

        public bool EtapaNaoDisponivelParaAplicacao { get; set; }

        public bool UsuarioLogadoEResponsavelAtualPelaSolicitacao { get; set; }

        public bool SolicitacaoServicoEDeMatricula { get; set; }

        public bool SituacaoAtualSolicitacaoEFimProcesso { get; set; }
        public bool TokenEntregaDocumentacao { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public PossuiBloqueio PossuiBloqueio { get; set; }

        public string NumeroProtocolo { get; set; }

        public string Processo { get; set; }

        public string EtapaAtual { get; set; }

        public string EtapaAtualCompleta { get; set; }

        public string GrupoEscalonamentoAtual { get; set; }

        public string SituacaoAtual { get; set; }

        public string Solicitante { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public DateTime? DataPrevistaSolucao { get; set; }

        public DateTime DataInicioProcesso { get; set; }

        public string DescricaoLookupSolicitacaoReduzida { get; set; }

        public string DescricaoLookupSolicitacao { get; set; }

        public string DescricaoTipoVinculoAluno { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public bool ConfigEtapaPossuiPaginaUploadDocumento { get; set; }
    }
}