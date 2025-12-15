using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaData : ISMCMappable, ISMCSeq
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

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public long SeqCicloLetivoInicio { get; set; }

        public long SeqCicloLetivoFim { get; set; }

        public long SeqTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

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

        public List<TurmaGrupoConfiguracaoData> GrupoConfiguracoesCompartilhadas { get; set; }

        #endregion [ Wizard1 ]

        #region [ Wizard2 ]

        public List<TurmaOfertaMatrizData> TurmaOfertasMatriz { get; set; }

        #endregion [ Wizard2 ]

        #region [ Wizard3 ]

        public List<TurmaParametrosData> TurmaParametros { get; set; }

        #endregion [ Wizard3 ]

        #region [ Wizard4 ]

        public List<TurmaDivisoesData> TurmaDivisoes { get; set; }

        #endregion

        #region [ Wizard5 ]

        public long? SeqComponenteCurricularAssunto { get; set; }

        #endregion

        #region [ Navigation ]

        public OrigemAvaliacaoData OrigemAvaliacao { get; set; }

        public List<TurmaGrupoConfiguracaoData> ConfiguracoesComponente { get; set; }

        public List<DivisaoTurmaData> DivisoesTurma { get; set; }

        public List<TurmaHistoricoSituacaoData> HistoricosSituacao { get; set; }

        public TurmaHistoricoSituacaoData HistoricosSituacaoAtual { get; set; }

        public List<TurmaCabecalhoConfiguracoesData> TurmaConfiguracoesCabecalho { get; set; }

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

        public bool PeriodoLetivoReadOnly { get; set; }

        public long? SeqAgendaTurma { get; set; }
    }
}
