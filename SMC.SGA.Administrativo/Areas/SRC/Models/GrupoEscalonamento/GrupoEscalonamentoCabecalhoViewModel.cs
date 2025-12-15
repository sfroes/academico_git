using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }

        public bool Ativo { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public bool ProcessoExpirado { get; set; }

        public bool PossuiParcelas { get; set; }

        public bool TodasEtapasEncerradas { get; set; }

        public bool NaoPermiteAssociarSolicitacaoEtapasExpiradas { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        public bool? PermitirNotificacao { get; set; }

        public bool PossuiParcelasVencimentoMenorEscalonamento { get; set; }

        #region HabilitacaoBotoes

        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        public bool EsconderBotaoValidarGrupo
        {
            get
            {
                return Ativo;
            }
        }

        public bool HabilitarBotaoValidarGrupo
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return false;
                }

                if (!PossuiParcelas)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoValidarGrupo
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return "MSG_Processo_Encerrado";
                }

                if (!PossuiParcelas)
                {
                    return "Botao_Instruction_Grupo_Escalonamento_Sem_Parcelas";
                }

                return string.Empty;
            }
        }

        public bool HabilitarBotaoAssociacao
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return false;
                }

                if (!Ativo)
                {
                    return false;
                }

                if (PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.NaoPermite)
                {
                    return false;
                }

                if (ProcessoExpirado)
                {
                    return false;
                }

                if (NaoPermiteAssociarSolicitacaoEtapasExpiradas)
                {
                    return false;
                }

                if (TodasEtapasEncerradas)
                {
                    return false;
                }

                //TRECHO COMENTADO POIS ESSA VALIDAÇÃO É FEITA AO ATIVAR O GRUPPO DE ESCALONAMENTO
                //if (PossuiParcelasVencimentoMenorEscalonamento)
                //{
                //    return false;
                //}

                return true;
            }
        }

        public string MensagemBotaoAssociacao
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return "Botao_Instruction_Nao_Permite_Associar_Solicitacao_Processo_Encerrado";
                }

                if (!Ativo)
                {
                    return "Botao_Instruction_Grupo_Escalonamento_Inativo";
                }

                if (PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.NaoPermite)
                {
                    return "Botao_Instruction_Nao_Permite_Associar_Servico_Reabrir";
                }

                if (ProcessoExpirado)
                {
                    return "Botao_Instruction_Nao_Permite_Associar_Solicitacao_Processo_Fora_Vigencia";
                }

                if (NaoPermiteAssociarSolicitacaoEtapasExpiradas)
                {
                    return "Botao_Instruction_Nao_Permite_Associar_Solicitacao_Etapas_Expiradas";
                }

                if (TodasEtapasEncerradas)
                {
                    return "Botao_Instruction_Nao_Permite_Associar_Solicitacao_Etapas_Encerradas";
                }

                //TRECHO COMENTADO POIS ESSA VALIDAÇÃO É FEITA AO ATIVAR O GRUPPO DE ESCALONAMENTO
                //if (PossuiParcelasVencimentoMenorEscalonamento)
                //{
                //    return "Botao_Instruction_Nao_Permite_Associar_Parcelas_Vencimento_Escalonamento";
                //}

                return null;
            }
        }

        public bool HabilitarBotaoNotificacao
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return false;
                }

                if (!Ativo)
                {
                    return false;
                }

                if (ProcessoExpirado)
                {
                    return false;
                }

                if (NaoPermiteAssociarSolicitacaoEtapasExpiradas)
                {
                    return false;
                }

                if (TodasEtapasEncerradas)
                {
                    return false;
                }

                if (QuantidadeSolicitacoes == 0)
                {
                    return false;
                }

                if (PermitirNotificacao != true)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoNotificacao
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Processo_Encerrado";
                }

                if (!Ativo)
                {
                    return "MSG_Grupo_Escalonamento_Inativo";
                }

                if (ProcessoExpirado)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Processo_Fora_Vigencia";
                }

                if (NaoPermiteAssociarSolicitacaoEtapasExpiradas)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Etapas_Expiradas";
                }

                if (TodasEtapasEncerradas)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Etapas_Encerradas";
                }

                if (QuantidadeSolicitacoes == 0)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Zero_Solicitacao";
                }

                if (PermitirNotificacao != true)
                {
                    return "Botao_Instruction_Nao_Permite_Notificacao_Tipo";
                }

                return null;
            }
        }

        public bool HabilitarBotaoCopiaProcesso
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return false;
                }

                if (!Ativo)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoCopiaProcesso
        {
            get
            {
                if (ProcessoEncerrado)
                {
                    return "MSG_Processo_Encerrado";
                }

                if (!Ativo)
                {
                    return "MSG_Grupo_Escalonamento_Inativo";
                }

                return null;
            }
        }

        #endregion
    }
}