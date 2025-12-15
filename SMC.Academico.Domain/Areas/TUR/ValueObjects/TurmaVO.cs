using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaVO : ISMCMappable, ISMCSeq
    {
        #region [ Operacoes Copiar Desdobrar Turma ]

        public long? SeqTurma { get; set; }

        public long? SeqTurmaOrigem { get; set; }

        public OperacaoTurma Operacao { get; set; }

        /// <summary>
        /// Recurso Emergencial, devido a falha no framework de preservar a última operação do dynamic
        /// </summary>
        public OperacaoTurma OperacaoFix { get; set; }

        #endregion [ Operacoes Copiar Desdobrar Turma ]

        #region [ Wizard0 ]

        public long Seq { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string DescricaoTurmaFormatado { get { return $"{Codigo}.{Numero} - {DescricaoConfiguracaoComponente}"; } }

        public long SeqCicloLetivoInicio { get; set; }

        public long SeqCicloLetivoFim { get; set; }

        public long SeqTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public long ConfiguracaoComponente { get; set; }

        public short? QuantidadeVagas { get; set; }

        public DateTime? DataInicioPeriodoLetivo { get; set; }

        public DateTime? DataFimPeriodoLetivo { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }

        public bool ConfirmarAlteracao { get; set; }

        public bool DiarioFechado { get; set; }

        #endregion [ Wizard0 ]

        #region [ Wizard1 ]

        public List<TurmaGrupoConfiguracaoVO> GrupoConfiguracoesCompartilhadas { get; set; }

        #endregion [ Wizard1 ]

        #region [ Wizard2 ]

        public List<TurmaOfertaMatrizVO> TurmaOfertasMatriz { get; set; }

        #endregion [ Wizard2 ]

        #region [ Wizard3 ]

        public List<TurmaParametrosVO> TurmaParametros { get; set; }

        #endregion [ Wizard3 ]

        #region [ Wizard4 ]

        public List<TurmaDivisoesVO> TurmaDivisoes { get; set; }

        #endregion

        #region [ Wizard5 ]

        public long? SeqComponenteCurricularAssunto { get; set; }

        #endregion

        #region [ Navigation ]

        public OrigemAvaliacaoVO OrigemAvaliacao { get; set; }

        public List<TurmaGrupoConfiguracaoVO> ConfiguracoesComponente { get; set; }

        public List<DivisaoTurmaVO> DivisoesTurma { get; set; }

        public List<TurmaHistoricoSituacaoVO> HistoricosSituacao { get; set; }

        public List<TurmaHistoricoFechamentoDiarioVO> HistoricosFechamentoDiario { get; set; }

        public TurmaHistoricoSituacaoVO HistoricosSituacaoAtual { get; set; }

        public List<TurmaCabecalhoConfiguracoesVO> TurmaConfiguracoesCabecalho { get; set; }

        #endregion [ Navigation ]

        #region [ Propriedades de Condição/Status dos Campos ]

        public bool HabilitarTipoTurma { get; set; }

        public bool HabilitarCicloLetivoInicio { get; set; }

        public bool HabilitarCicloLetivoFim { get; set; }

        public bool HabilitarConfiguracaoComponente { get; set; }

        public bool HabilitarGrupoConfiguracoesCompartilhadas { get; set; }

        /// - Passo 2: Inclusão de configuração para compartilhamento (não é permitido desmarcar uma configuração já selecionada). 
        public bool HabilitarGrupoConfiguracoesCompartilhadasDesmarcar { get; set; }

        public bool HabilitarComponenteCurricularAssunto { get; set; }

        public bool HabilitarLocalidades { get; set; }

        public bool HabilitarInformacoesAdicionais { get; set; }

        #endregion [ Propriedades de Condição/Status dos Campus ]

        //Existe solicitação de matrícula ou plano de estudo?
        public bool ExisteSolicitacaoMatriculaOuPlanoEstudo { get; set; }

        /// <summary>
        /// Verificar se a turma existe na solicitação de serviço ou em 
        /// algum plano de estudo. Caso existir, o comando deve ser exibido desabilitado.
        /// </summary>
        public bool DesativarExcluir { get; set; }

        public bool PossuiNotaLancada { get; set; }

        public bool EhOuPossuiDesdobramento { get; set; }

        public bool PeriodoLetivoReadOnly { get; set; }

        public long? SeqAgendaTurma { get; set; }
    }
}
