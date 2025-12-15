using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Models
{
    public class HomeTurmaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }

        public List<HomeTurmaDivisaoViewModel> DivisoesTurma { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool ResponsavelTurma { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        public bool? PermiteLancamentoFrequencia { get; set; }

        public bool EsconderNotasFrequenciasFinais
        {
            get
            {
                if (PermiteAvaliacaoParcial.GetValueOrDefault())
                    return true;

                return false;
            }
        }

        public bool HabilitarNotasFrequenciasFinais
        {

            get
            {
                if (!PermiteAvaliacaoParcial.GetValueOrDefault() && DiarioFechado)
                    return false;

                return true;
            }
        }

        public string MensagemNotasFrequenciasFinais
        {
            get
            {
                if (!PermiteAvaliacaoParcial.GetValueOrDefault() && DiarioFechado)
                {
                    return "MensagemDesabilitaBotaoNotasFrequenciasFinal";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Esconde se não tiver avaliação parcial
        /// </summary>
        public bool HideLancamentoNota
        {
            get
            {
                if (!PermiteAvaliacaoParcial.GetValueOrDefault()) { return true; }
                return false;
            }
        }


        /// <summary>
        /// Desabilitar se professor responsável ou diário não fechado
        /// </summary>
        public bool DesabilitarLancamentoNota
        {
            get
            {
                bool retorno = !ResponsavelTurma ? false : true;
                return retorno;
            }
        }

        /// <summary>
        /// Esconde se não tiver avaliação parcial
        /// </summary>
        public bool HideLAcompanhamentoNotas
        {
            get
            {
                if (!PermiteAvaliacaoParcial.GetValueOrDefault()) { return true; }
                return false;
            }
        }

        /// <summary>
        /// Desabilitar se professor responsável ou diário não fechado
        /// </summary>
        public bool DesabilitarAcompanhamentoNotas
        {
            get
            {
                bool retorno = !ResponsavelTurma ? false : true;
                return retorno;
            }
        }

        public string InstructionsAcompanhamentoNotas
        {
            get
            {
                string retorno = !ResponsavelTurma ? "MensagemDesabilitaBotaoAcompanhamentoNotas" : string.Empty;
                return retorno;
            }
        }

        /// <summary>
        /// Se botão lançamento de notas desabilitados exibir instrução: "Disponível somente para professor responsável pela turma com diário aberto"
        /// </summary>
        public string InstructionsLancamentoNota
        {
            get
            {
                string retorno = (!ResponsavelTurma || DiarioFechado) ? "MensagemDesabilitaBotaoLancamentoNotas" : string.Empty;
                return retorno;
            }
        }

        /// <summary>
        /// Esconde se não tiver avaliação parcial
        /// </summary>
        public bool HideAvaliacao
        {
            get
            {
                if (!PermiteAvaliacaoParcial.GetValueOrDefault()) { return true; }
                return false;
            }
        }

        /// <summary>
        /// Desabilita se professor responsável ou diário não fechado
        /// </summary>
        public bool DesabilitarAvaliacao
        {
            get
            {
                bool retorno = (!ResponsavelTurma || DiarioFechado) ? false : true;
                return retorno;
            }
        }

        /// <summary>
        /// Se botão avaliação desabilitados exibir instrução: "Disponível somente para professor responsável pela turma com diário aberto"
        /// </summary>
        public string InstructionsAvaliacao
        {
            get
            {
                string retorno = (!ResponsavelTurma || DiarioFechado) ? "MensagemDesabilitaBotaoAvaliacao" : string.Empty;
                return retorno;
            }
        }
    }
}