using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class DivisaoTurmaColaboradorProfessorViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTurma { get; set; }

        [SMCHidden]
        public long SeqColaborador { get; set; }
               
        public string NomeColaborador { get; set; }

        public short QuantidadeCargaHoraria { get; set; }

        public bool GeraOrientacao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool? TurmaPossuiAgenda { get; set; }

        public short? CargaHorariaGrade { get; set; }

        #region Habilitar de botões e mensagens

        public bool HabilitarBotaoEditar
        {
            get
            {
                if (DiarioFechado)
                {
                    return false;
                }
                else if (TurmaPossuiAgenda.Value && CargaHorariaGrade.GetValueOrDefault() > 0)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoEditar
        {
            get
            {
                if (DiarioFechado)
                {
                    return "MSG_Diario_Fechado";
                }
                else if (TurmaPossuiAgenda.Value && CargaHorariaGrade.GetValueOrDefault() > 0)
                {
                    return "MSG_Turma_Possui_Agenda";
                }

                return string.Empty;
            }
        }

        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (DiarioFechado)
                {
                    return false;
                }
                else if (TurmaPossuiAgenda.Value && CargaHorariaGrade.GetValueOrDefault() > 0)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoExcluir
        {
            get
            {
                if (DiarioFechado)
                {
                    return "MSG_Diario_Fechado";
                }
                else if (TurmaPossuiAgenda.Value && CargaHorariaGrade.GetValueOrDefault() > 0)
                {
                    return "MSG_Turma_Possui_Agenda";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}