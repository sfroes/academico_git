using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string CodigoFormatado { get { return $"{Codigo}.{Numero}"; } }

        public string DescricaoTipoTurma { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        [SMCDescription]
        public string DescricaoTurmaCompleto { get { return $"{CodigoFormatado} - {DescricaoConfiguracaoComponente}"; } }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoCicloLetivoFim { get; set; }

        public short? QuantidadeVagas { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string SituacaoJustificativa { get; set; }

        public List<TurmaOfertaMatrizViewModel> TurmaOfertasMatriz { get; set; }

        public List<TurmaDivisoesViewModel> TurmaDivisoes { get; set; }

        public List<TurmaCabecalhoConfiguracoesViewModel> TurmaConfiguracoesCabecalho { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get { return LegendaPrincipal.Principal; } }

        public bool ConfirmarAlteracao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool PossuiNotaLancada { get; set; }

        public bool EhOuPossuiDesdobramento { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string DiarioSituacao => (DiarioFechado ? SituacaoTurmaDiario.Fechado : SituacaoTurmaDiario.Aberto).SMCGetDescription();

        /// <summary>
        /// Verificar se a turma existe na solicitação de serviço ou em algum plano de estudo. Caso existir, o comando deve ser exibido desabilitado
        /// </summary>
        public bool DesativarExcluir { get; set; }

        /// <summary>
        /// Verificar se existem eventos de aula criados para as divisões da turma. Caso existir, o comando deve ser desabilitado. 
        /// </summary>
        public bool DesativarExcluirValidacaoEventoAula { get; set; }

        public string MensagemDesativarExcluir { get; set; }

        /// <summary>
        /// Verificar se existe aula lançada para esta turma
        /// </summary>
        public bool AulaLancada { get; set; }

        public bool PermiteAvaliacaoParcial { get; set; }

        public bool AgendaTurmaConfigurada { get; set; }

        public bool GradeConfigurada { get; set; }

        public bool PossuiDivisaoOrientacao { get; set; }
    }
}