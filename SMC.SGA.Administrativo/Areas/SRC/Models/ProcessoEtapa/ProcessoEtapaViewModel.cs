using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoEtapa { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public bool ExibirAcessoEtapa { get; set; }

        public string Token { get; set; }

        public List<EscalonamentoViewModel> Escalonamentos { get; set; }

        public List<ProcessoEtapaConfiguracaoNotificacaoViewModel> ConfiguracoesNotificacao { get; set; }

        public short Ordem { get; set; }

        #region Ações dos Botoes

        [SMCIgnoreProp]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        [SMCIgnoreProp]
        public bool ExibirBotaoLiberar
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.EmManutencao || SituacaoEtapa == SituacaoEtapa.AguardandoLiberacao)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCIgnoreProp]
        public bool ExibirBotaoManutencao
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    HabilitarBotaoManuntecao = false;
                    MensagemInstruictionBotaoManutencao = "boto_manutencao_situacao_encerrada_instructions";
                    return false;
                }
                return true;
            }
        }

        [SMCIgnoreProp]
        public bool HabilitarBotaoManuntecao { get; set; } = true;

        [SMCIgnoreProp]
        public string MensagemInstruictionBotaoManutencao { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovaConfiguracaoNotificacao
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoNovaConfiguracaoNotificacao
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "Botao_Instruction_Nova_Configuracao_Etapa_Encerrada";
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "Botao_Instruction_Nova_Configuracao_Etapa_Liberada";
                }

                return null;
            }
        }

        #endregion
    }
}
