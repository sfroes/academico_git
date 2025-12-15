using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        public string DescricaoEtapa { get; set;  }

        public SituacaoEtapa SituacaoEtapa { get; set; }

        public List<ConfiguracaoEtapaListarItemViewModel> ConfiguracoesEtapa { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool EsconderBotaoLiberar
        {
            get
            {
                return !(SituacaoEtapa == SituacaoEtapa.AguardandoLiberacao || SituacaoEtapa == SituacaoEtapa.EmManutencao);
            }
        }

        [SMCHidden]
        public bool EsconderBotaoManutencao
        {
            get
            {
                return !(SituacaoEtapa == SituacaoEtapa.Liberada || SituacaoEtapa == SituacaoEtapa.Encerrada);
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoManutencao
        {
            get
            {
                return !(SituacaoEtapa == SituacaoEtapa.Encerrada);
            }
        }

        [SMCHidden]
        public string MensagemBotaoManutencao
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotaoManutencao";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoNovo
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
        public string MensagemBotaoNovo
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotao_EtapaEncerrada";
                }
                else if(SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotao_EtapaLiberada";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}