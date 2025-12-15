using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.TUR.Views.Turma.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string CodigoFormatado { get; set; }

        public string CicloLetivoInicio { get; set; }

        public string CicloLetivoFim { get; set; }

        public short Vagas { get; set; }   
        
        public string DescricaoTipoTurma { get; set; }

        public SituacaoTurma SituacaoTurmaAtual { get; set; }

        [SMCConditionalDisplay(nameof(SituacaoTurmaAtual), SMCConditionalOperation.Equals, SituacaoTurma.Cancelada)]
        [SMCConditionalRequired(nameof(SituacaoTurmaAtual), SMCConditionalOperation.Equals, SituacaoTurma.Cancelada)]
        [SMCMultiline]
        public string SituacaoJustificativa { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta smc-sga-mensagem-multiplas-linhas")]
        [SMCConditionalDisplay(nameof(SituacaoTurmaAtual), SMCConditionalOperation.Equals, SituacaoTurma.Cancelada)]
        public string MensagemSituacaoCancelada { get { return UIResource.MSG_Informacao_Turma_Cancelada; } }

        public List<TurmaCabecalhoResponsavelViewModel> Colaboradores { get; set; }

        public List<TurmaCabecalhoConfiguracoesViewModel> TurmaConfiguracoesCabecalho { get; set; }

        public List<TurmaCabecalhoDivisoesViewModel> TurmaDivisoesCabecalho { get; set; }

        [SMCHidden]
        public bool DiarioFechado { get; set; }

        public string DescricaoTurma { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

        public long SeqComponenteCurricular { get; set; }

        #region Habilitar botões e mensagens

        public bool HabilitarBotaoAssociarProfessor
        {
            get
            {
                if (DiarioFechado)
                {
                    return false;
                }

                return true;
            }
        }

        public string MensagemBotaoAssociarProfessor
        {
            get
            {
                if (DiarioFechado)
                {
                    return "MSG_Diario_Fechado";
                }

                return string.Empty;
            }
        }

        #endregion  
    }
}