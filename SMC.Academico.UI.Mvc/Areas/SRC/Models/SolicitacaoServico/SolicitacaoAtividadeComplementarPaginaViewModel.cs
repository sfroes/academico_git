using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
	public class SolicitacaoAtividadeComplementarPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
	{
		[SMCHidden]
		public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_ATIVIDADE_COMPLEMENTAR;

		#region Datasources

		[SMCDataSource]
		public List<SMCSelectListItem> DivisoesComponente { get; set; }

		[SMCDataSource]
		public List<SMCDatasourceItem> CiclosLetivos { get; set; }

		[SMCDataSource]
		public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

		#endregion Datasources

		[SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid8_24)]
		[SMCRequired]
		[SMCSelect(nameof(DivisoesComponente))]
		public long SeqDivisaoComponente { get; set; }

		[SMCHidden]
		public bool Artigo { get; set; }

		[SMCRequired]
		[SMCSelect(nameof(CiclosLetivos), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoCicloLetivo))]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		public long? SeqCicloLetivo { get; set; }

		public string DescricaoCicloLetivo { get; set; }

		[SMCRequired]
		[SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
		[SMCMaxLength(255)]
		public string Descricao { get; set; }

		[SMCMinValue(1)]
		[SMCMask("9999")]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		public int? CargaHoraria { get; set; }

		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		[SMCMaxDate(nameof(DataFim))]
		[SMCConditionalRequired(nameof(CargaHoraria), SMCConditionalOperation.GreaterThen, 0)]
		public DateTime? DataInicio { get; set; }

		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		[SMCMinDate(nameof(DataInicio))]
		[SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "", RuleName = "R1")]
		[SMCConditionalRequired(nameof(CargaHoraria), SMCConditionalOperation.GreaterThen, 0, RuleName = "R2")]
		[SMCConditionalRule("R1 || R2")]
		public DateTime? DataFim { get; set; }

		[SMCRadioButtonList]
		[SMCSize(SMCSize.Grid24_24)]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R2 || R3")]
		[SMCConditionalReadonly(nameof(SeqDivisaoComponente), SMCConditionalOperation.NotEqual, "true", DataAttribute = "artigo")]
		[SMCConditionalDisplay(nameof(SeqDivisaoComponente), "true", DataAttribute = "artigo")]
		public TipoPublicacao? TipoPublicacao { get; set; }

		#region [ Tipo Publicação - Conferência ]

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R1 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia)]
		[SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid14_24)]
		public string DescricaoEvento { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R1 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia)]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
		[SMCMask("0000")]
		public int? AnoRealizacaoEvento { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R1 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia)]
		[SMCSelect]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
		public NaturezaArtigo? NaturezaArtigo { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R1 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia)]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
		[SMCSelect]
		public TipoEvento? TipoEvento { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRequired(nameof(TipoEvento), SMCConditionalOperation.NotEqual, Common.Areas.CUR.Enums.TipoEvento.Internacional, RuleName = "R4")]
		[SMCConditionalRule("R1 && R4 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Conferencia)]
		[SMCMapForceFromTo]
		[SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
		[StateCity]
		[SMCConditionalReadonly(nameof(TipoEvento), Common.Areas.CUR.Enums.TipoEvento.Internacional)]
		public EstadoCidadeViewModel EstadoCidade { get; set; } = new EstadoCidadeViewModel();

		#endregion [ Tipo Publicação - Conferência ]

		#region [ Tipo Publicação - Periódico ]

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Submetido, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R3")]
		[SMCConditionalRule("R1 && (R2 || R3)")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico)]
		[SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
		[PeriodicoCapesLookup(true)]
		public PeriodicoCapesLookupViewModel SeqPeriodico { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRule("R1 && R2")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico)]
		[SMCSize(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
		public int? NumeroVolumePeriodico { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRule("R1 && R2")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico)]
		[SMCSize(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
		public int? NumeroFasciculoPeriodico { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRule("R1 && R2")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico)]
		[SMCSize(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
		public int? NumeroPaginaInicialPeriodico { get; set; }

		[SMCConditionalRequired(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico, RuleName = "R1")]
		[SMCConditionalRequired(nameof(SeqDivisaoComponente), ComprovacaoArtigo.Publicado, DataAttribute = "minimo", RuleName = "R2")]
		[SMCConditionalRule("R1 && R2")]
		[SMCConditionalDisplay(nameof(TipoPublicacao), Common.Areas.CUR.Enums.TipoPublicacao.Periodico)]
		[SMCSize(SMCSize.Grid3_24, SMCSize.Grid12_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
		public int? NumeroPaginaFinalPeriodico { get; set; }

		#endregion [ Tipo Publicação - Periódico ]

		#region [Lançamento de nota]

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public decimal NotaMaxima { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public bool ApuracaoNota { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public bool ApuracaoFrequencia { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public bool ApuracaoEscala { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public long? SeqEscalaApuracao { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public short? PercentualFrequenciaAprovado { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public short? PercentualNotaAprovado { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarDadosConfiguracaoDivisaoComponente), null, true, new string[] { nameof(SeqPessoaAtuacao) })]
		public bool PermiteAlunoSemNota { get; set; }


		[SMCConditionalDisplay(nameof(ApuracaoNota), SMCConditionalOperation.Equals, true)]
		[SMCConditionalRequired(nameof(ApuracaoNota), SMCConditionalOperation.Equals, true)]
		[SMCDecimalDigits(2)]
		[SMCMaxValue(nameof(NotaMaxima))]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		public decimal? Nota { get; set; }

		[SMCConditionalDisplay(nameof(ApuracaoNota), SMCConditionalOperation.Equals, false, RuleName = "R1")]
		[SMCConditionalDisplay(nameof(ApuracaoEscala), SMCConditionalOperation.Equals, true, RuleName = "R2")]
		[SMCConditionalRule("R1 || R2")]
		[SMCConditionalRequired(nameof(ApuracaoEscala), SMCConditionalOperation.Equals, true)]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid7_24, SMCSize.Grid7_24, SMCSize.Grid5_24)]
		[SMCSelect(nameof(EscalaApuracaoItens), autoSelectSingleItem: true)]
		[SMCDependency(nameof(Nota), nameof(SolicitacaoServicoFluxoBaseController.BuscarEscalaApuracaoPorNota), null, false, new string[] { nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao) })]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarEscalaApuracaoPorNota), null, true, new string[] { nameof(Nota), nameof(SeqPessoaAtuacao) })]
		[SMCConditionalReadonly(nameof(ApuracaoNota), SMCConditionalOperation.Equals, true, PersistentValue = true)]
		public long? SeqEscalaApuracaoItem { get; set; }

		[SMCConditionalDisplay(nameof(ApuracaoFrequencia), SMCConditionalOperation.Equals, true)]
		[SMCConditionalRequired(nameof(ApuracaoFrequencia), SMCConditionalOperation.Equals, true)]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		public int? Faltas { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, true, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(Nota), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(SeqEscalaApuracaoItem), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(Faltas), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(SeqEscalaApuracao), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PercentualFrequenciaAprovado), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PercentualNotaAprovado), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PermiteAlunoSemNota), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado) })]
		public SituacaoHistoricoEscolar? SituacaoFinal { get; set; }

		[SMCReadOnly]
		[SMCDependency(nameof(SeqDivisaoComponente), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, true, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(Nota), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(SeqEscalaApuracaoItem), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(Faltas), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(SeqEscalaApuracao), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PercentualFrequenciaAprovado), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualNotaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PercentualNotaAprovado), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PermiteAlunoSemNota) })]
		[SMCDependency(nameof(PermiteAlunoSemNota), nameof(SolicitacaoServicoFluxoBaseController.BuscarSituacaoFinal), null, false, new string[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(Nota), nameof(SeqDivisaoComponente), nameof(SeqPessoaAtuacao), nameof(SeqEscalaApuracao), nameof(PercentualFrequenciaAprovado), nameof(PercentualNotaAprovado) })]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
		public string DescricaoSituacaoFinal
		{
			get
			{
				return SituacaoFinal.HasValue ? SMCEnumHelper.GetDescription(SituacaoFinal) : null;
			}
		}

		#endregion [Lançamento de nota]
	}
}