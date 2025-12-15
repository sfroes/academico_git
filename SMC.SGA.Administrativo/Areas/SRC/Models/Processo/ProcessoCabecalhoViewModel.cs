using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }

        public long SeqGrupoEscalonamento { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public string DescricaoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFim { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCValueEmpty("-")]
        public DateTime? DataEncerramento { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public int QuantidadeSolicitacoesProcesso { get; set; }

        [SMCHidden]
        public bool ExibirQuantidade { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataInicioEscalonamento { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataFimEscalonamento { get; set; }

        public short? NumeroDivisaoParcelas { get; set; }

        public bool Ativo { get; set; }

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
                return !ProcessoEncerrado;
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

                return string.Empty;
            }
        }
    }
}