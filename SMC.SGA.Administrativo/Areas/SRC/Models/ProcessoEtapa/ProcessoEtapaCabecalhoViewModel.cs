using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        [SMCHidden]
        public long SeqCicloLetivo { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoCicloLetivo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }

        [SMCHidden]
        public long SeqEtapaSgf { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }

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

        #endregion
    }
}