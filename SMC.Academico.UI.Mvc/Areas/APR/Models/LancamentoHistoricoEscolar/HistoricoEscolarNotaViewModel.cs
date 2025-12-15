using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.UI.Mvc.Areas.APR.Views.LancamentoHistoricoEscolar.App_LocalResources;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class HistoricoEscolarNotaViewModel : SMCViewModelBase, ISMCMappable
	{
		[SMCHidden]
		public long Seq { get; set; }

		[SMCHidden]
		public long SeqPessoaAtuacao { get; set; }

		[SMCHidden]
		public long SeqAlunoHistorico { get; set; }

		[SMCHidden]
		public bool IndicadorApuracaoFrequencia { get; set; }

		[SMCHidden]
		public TipoArredondamento? TipoArredondamento { get; set; }

		[SMCHidden]
		public bool IndicadorApuracaoNota { get; set; }

		[SMCHidden]
		public bool IndicadorPermiteAlunoSemNota { get; set; }

		[SMCHidden]
		public long? SeqEscalaApuracao { get; set; }

		[SMCHidden]
		public short? NotaMaxima { get; set; }

		[SMCHidden]
		public short? PercentualMinimoFrequencia { get; set; }

		[SMCHidden]
		public short? PercentualMinimoNota { get; set; }

		[SMCHidden]
		public short? CargaHoraria { get; set; }

		[SMCSize(SMCSize.Grid2_24)]
		public short? CargaHorariaRealizada { get; set; }

		[SMCHidden]
		public long SeqComponenteCurricular { get; set; }

		[SMCHidden]
		public long? SeqComponenteCurricularAssunto { get; set; }

		[SMCHidden]
		public short? Credito { get; set; }

		[SMCHidden]
		public bool? Aprovado { get; set; }

		#region [ Configuração critério avaliação ]

		[SMCIgnoreProp]
		public bool MostrarLacamentosSemNota => IndicadorPermiteAlunoSemNota;

		/// <summary>
		/// Somente apuração por nota, exibir somente o campo "Nota" para preenchimento
		/// </summary>
		public bool MostrarLancamentosNota => SeqEscalaApuracao.GetValueOrDefault() == 0 && IndicadorApuracaoNota;

		/// <summary>
		/// Apuração por nota e escala de apuração, exibir o campo "Nota" para preenchimento e o campo "Apuração" desabilitado e preenchido automaticamente
		/// com a descrição do item de escala de apuração referente à nota digitada.
		/// </summary>
		public bool MostrarLancamentosNotaDescricao => SeqEscalaApuracao.GetValueOrDefault() > 0 && IndicadorApuracaoNota;

		/// <summary>
		/// Somente escala de apuração, exibir somente o campo "Apuração" habilitado para edição e para seleção exibir os itens da escala de apuração.
		/// </summary>
		public bool MostrarLancamentosSeqEscalaApuracaoItem => SeqEscalaApuracao.GetValueOrDefault() > 0 && !IndicadorApuracaoNota;

		#endregion [ Configuração critério avaliação ]

		[SMCReadOnly]
		public long NumeroRegistroAcademico { get; set; }

		[SMCIgnoreProp]
		public string NomeAluno { get; set; }

        [SMCReadOnly]
        public string NomeAlunoFormatado => AlunoFormado ? $"{NomeAluno} - {UIResource.AlunoFormado}" : NomeAluno;

        [SMCConditionalDisplay(nameof(IndicadorPermiteAlunoSemNota), true)]
		[SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
		// Se possuir um registro de nota (Seq) e não possuir uma nota lançada, o flag "sem nota" foi marcado como true.
		public bool SemNota { get; set; }

		[SMCConditionalReadonly(nameof(SemNota), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "N1")]
		[SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "N2")]
		[SMCConditionalRule("N1 || N2")]
		[SMCMaxValue(nameof(NotaMaxima))]
		[SMCMinValue(0)]
		public short? Nota { get; set; }

		[SMCConditionalReadonly(nameof(SemNota), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "A1")]
		[SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "A2")]
		[SMCConditionalRule("A1 || A2")]
		[SMCSelect(nameof(LancamentoHistoricoEscolarViewModel.ListaEscalaApuracaoItens))]
		public long? SeqEscalaApuracaoItem { get; set; }

		[SMCReadOnly]
		public string DescricaoEscalaApuracaoItem { get; set; }

		//[SMCConditionalReadonly(nameof(IndicadorApuracaoFrequencia), SMCConditionalOperation.Equals, false, PersistentValue = true)]
		//[SMCMaxValue(nameof(CargaHoraria))]
		//[SMCMinValue(0)]
		[SMCHidden]
		public short? Faltas { get; set; }

		[SMCReadOnly]
		public short? SomaFaltasApuracao { get; set; }

		//[SMCDependency(nameof(Faltas), nameof(LancamentoHistoricoEscolarController.CalcularSituacaoFinalAluno), "LancamentoHistoricoEscolar", "APR", false, new string[] { nameof(SeqEscalaApuracao), nameof(Nota), nameof(IndicadorApuracaoNota), nameof(IndicadorApuracaoFrequencia), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(NotaMaxima), nameof(CargaHoraria), nameof(IndicadorPermiteAlunoSemNota) })]
		//[SMCDependency(nameof(SemNota), nameof(LancamentoHistoricoEscolarController.CalcularSituacaoFinalAluno), "LancamentoHistoricoEscolar", "APR", false, new string[] { nameof(SeqEscalaApuracao), nameof(Nota), nameof(IndicadorApuracaoNota), nameof(IndicadorApuracaoFrequencia), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(NotaMaxima), nameof(CargaHoraria), nameof(IndicadorPermiteAlunoSemNota) })]
		//[SMCDependency(nameof(Nota), nameof(LancamentoHistoricoEscolarController.CalcularSituacaoFinalAluno), "LancamentoHistoricoEscolar", "APR", false, new string[] { nameof(SeqEscalaApuracao), nameof(Faltas), nameof(IndicadorApuracaoNota), nameof(IndicadorApuracaoFrequencia), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(NotaMaxima), nameof(CargaHoraria), nameof(IndicadorPermiteAlunoSemNota) })]
		[SMCReadOnly]
		public string DescricaoSituacaoHistoricoEscolar { get; set; }

        [SMCConditionalReadonly(nameof(DescricaoSituacaoHistoricoEscolar), "")]
        [SMCMultiline]
        public string Observacao { get; set; }

        [SMCHidden]
		public bool AlunoFormado { get; set; }

        [SMCHidden]
        public bool AlunoDispensado { get; set; }

        [SMCHidden]
        public bool AlunoAprovado { get; set; }

        [SMCHidden]
        public bool SomenteLeitura => AlunoFormado || AlunoDispensado || AlunoAprovado;
    }
}