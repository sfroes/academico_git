using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Models
{
    public class HomeTurmaDivisaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
        public string DivisaoTurmaRelatorioDescricao { get; set; }
        public long SeqOrigemAvaliacao { get; set; }
        public bool DiarioFechado { get; set; }
        public bool? PermiteAvaliacaoParcial { get; set; }
        public bool? PermiteLancamentoFrequencia { get; set; }
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }
        /// <summary>
        /// Exibir somente se o diário estiver aberto e o indicador de PermiteAvaliacaoParcial da origem de avaliação da turma for igual a TRUE.
        /// </summary>
        public bool HideAvaliacao
        {
            get
            {
                bool retorno = (PermiteAvaliacaoParcial.GetValueOrDefault() && !DiarioFechado) ? false : true;

                return retorno;
            }
        }
        /// <summary>
        /// Exibir somente se o diário estiver aberto e o indicador de PermiteAvaliacaoParcial da origem de avaliação da turma for igual a TRUE.
        /// </summary>
        public bool HideLancamentoNota
        {
            get
            {
                bool retorno = (PermiteAvaliacaoParcial.GetValueOrDefault()) ? false : true;

                return retorno;
            }
        }
        /// <summary>
        /// Exibir somente se a divisão permite lançamento de frequência.
        /// </summary>
        public bool HideLancamentoFrequencia
        {
            get
            {
                bool retorno = !PermiteLancamentoFrequencia.GetValueOrDefault();

                return retorno;
            }
        }
        public string FillInsttuctionLacamentoFrequencia
        {
            get
            {
                string retorno = DiarioFechado ? "Diário fechado" : "";
                return retorno;
            }
        }
        public bool GerarOrientacao { get; set; }
        public short? CargaHoraria { get; set; }
        public string DescricaoComponenteCurricularOrganizacao { get; set; }
        public string TipoDivisaoDescricao { get; set; }
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }
        public string TurmaCodigoFormatado { get; set; }
        public string DescricaoFormatada { get; set; }
        public string DescricaoFormatadaSemNumero { get; set; }

    }
}