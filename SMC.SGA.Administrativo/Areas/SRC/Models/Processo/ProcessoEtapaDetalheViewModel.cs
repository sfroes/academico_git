using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaDetalheViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCKey]
        [SMCHidden]
        [SMCParameter]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        [SMCDescription]
        public string DescricaoEtapa { get; set; }

        [SMCHidden]
        public long SeqEtapaSgf { get; set; }

        [SMCValueEmpty("-")]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicio { get; set; }

        [SMCValueEmpty("-")]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        [SMCValueEmpty("-")]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime? DataEncerramento { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool ExibirAcessoEtapa { get; set; }

        [SMCHidden]
        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        [SMCHidden]
        public string Token { get; set; }

        [SMCHidden]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        [SMCHidden]
        public bool HabilitaEncerrarEtapa { get; set; }

        [SMCHidden]
        public string InstructionEncerrarEtapa { get; set; }

        [SMCHidden]
        public bool HabilitaExcluirEtapa { get; set; }

        [SMCHidden]
        public string InstructionExcluirEtapa { get; set; }

        #region Ações dos Botoes

        [SMCIgnoreProp]
        public bool ExibirBotaoLiberar {
            get
            {
                if(SituacaoEtapa == SituacaoEtapa.EmManutencao || SituacaoEtapa == SituacaoEtapa.AguardandoLiberacao)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCIgnoreProp]
        public bool ExibirBotaoManutencao {
            get
            {
                if(SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                if(SituacaoEtapa == SituacaoEtapa.Encerrada)
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

        #endregion
    }

}