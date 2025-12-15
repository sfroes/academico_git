using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
	public class AtendimentoIntercambioViewModel : SolicitacaoServicoPaginaViewModelBase
	{
		[SMCHidden]
		public override string Token => TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_INTERCAMBIO;

		[SMCDataSource]
		public List<SMCDatasourceItem> TiposOrientacoes { get; set; }

		[SMCDataSource]
		public List<SMCDatasourceItem> Colaboradores { get; set; }

		[SMCDataSource]
		public List<SMCDatasourceItem> TiposParticipacoes { get; set; }

		[SMCDataSource]
		public List<SMCDatasourceItem> TiposTermoIntercambio { get; set; }

		[SMCHidden]
		public long SeqNivelEnsino { get; set; }

		[SMCHidden]
		public string Cpf { get; set; }

		[SMCHidden]
		public string NumeroPassaporte { get; set; }

		[SMCHidden]
		public TipoMobilidade? TipoMobilidade { get; } = Academico.Common.Areas.ALN.Enums.TipoMobilidade.SaidaParaOutraInstituicao;

		[SMCHidden]
		public long SeqInstituicaoEnsino { get; set; }

		[SMCHidden]
		public long SeqTipoVinculoAluno { get; set; }

		[SMCSelect(nameof(TiposTermoIntercambio))]
		[SMCSize(Framework.SMCSize.Grid6_24)]
		public long? SeqTipoTermoIntercambio { get; set; }

		[SMCHidden]
		public bool CotutelaJaAssociada { get; set; }

		[TermoIntercambioLookup]
		[SMCDependency(nameof(Cpf))]
		[SMCDependency(nameof(NumeroPassaporte))]
		[SMCDependency(nameof(SeqInstituicaoEnsino))]
		[SMCDependency(nameof(SeqNivelEnsino))]
		[SMCDependency(nameof(SeqTipoVinculoAluno))]
		[SMCDependency(nameof(SeqTipoTermoIntercambio))]
		[SMCDependency(nameof(TipoMobilidade))]
		[SMCSize(Framework.SMCSize.Grid24_24)]
		[SMCConditionalReadonly(nameof(CotutelaJaAssociada), true, PersistentValue = true)]
		public TermoIntercambioLookupViewModel SeqTermoIntercambio { get; set; }

		[SMCReadOnly]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDadosTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		[SMCSize(Framework.SMCSize.Grid10_24)]
		public string DescricaoTipoTermo { get; set; }

		[SMCReadOnly]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDadosTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		[SMCSize(Framework.SMCSize.Grid14_24)]
		public string DescricaoInstituicaoExterna { get; set; }

		[SMCHidden]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDatasTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		public bool? ExigePeriodo { get; set; }

		/// <summary>
		/// Como é um atendimento de solicitação de intercâmbio, sempre terá apenas ZERO ou UM tipo de orientação parametrizado.
		/// Caso tenha um, considerar a parametrização de obrigatoriedade dele para definir required para o combo de tipo de orientação
		/// </summary>
		[SMCHidden]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDadosTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		public CadastroOrientacao OrientacaoAluno { get; set; }

		/// <summary>
		/// Como é um atendimento de solicitação de intercâmbio, sempre terá apenas ZERO ou UM tipo de orientação parametrizado.
		/// Caso tenha um, exibir o combo de tipo de orientação
		/// </summary>
		[SMCHidden]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDadosTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		public bool ExisteTipoOrientacaoParametrizado { get; set; }

		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDatasTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		[SMCConditionalReadonly(nameof(ExigePeriodo), SMCConditionalOperation.NotEqual, false, PersistentValue = true)]
		[SMCConditionalRequired(nameof(ExigePeriodo), false, RuleName = "R1")]
		[SMCConditionalRequired(nameof(CotutelaJaAssociada), true, RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqTipoTermoIntercambio), true, DataAttribute = "concedeformacao", RuleName = "R3")]
		[SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual, "", RuleName = "R4" )]
		[SMCConditionalRule("(R3 && R2) || (!R3 && R1) || R4")]
		[SMCSize(SMCSize.Grid5_24)]
		[SMCMaxDate(nameof(DataFim))]
		public DateTime? DataInicio { get; set; }

		[SMCReadOnly]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDatasTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		[SMCConditionalReadonly(nameof(ExigePeriodo), SMCConditionalOperation.NotEqual, false, PersistentValue = true)]
		[SMCConditionalRequired(nameof(ExigePeriodo), false, RuleName = "R1")]
		[SMCConditionalRequired(nameof(CotutelaJaAssociada), true, RuleName = "R2")]
		[SMCConditionalRequired(nameof(SeqTipoTermoIntercambio), true, DataAttribute = "concedeformacao", RuleName = "R3")]
		[SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "", RuleName = "R4")]
		[SMCConditionalRule("(R3 && R2) || (!R3 && R1) || R4")]
		[SMCSize(SMCSize.Grid5_24)]
		[SMCMinDate(nameof(DataInicio))]
		public DateTime? DataFim { get; set; }

		[SMCSelect(nameof(TiposOrientacoes), autoSelectSingleItem: true)]
		[SMCSize(Framework.SMCSize.Grid8_24)]
		[SMCConditionalReadonly(nameof(Participantes), SMCConditionalOperation.NotEqual, null, PersistentValue = true, RuleName = "R1")]
		[SMCConditionalReadonly(nameof(SeqTermoIntercambio), SMCConditionalOperation.Equals, null, PersistentValue = true, RuleName = "R2")]
		[SMCConditionalReadonly(nameof(CotutelaJaAssociada), true, RuleName = "R3", PersistentValue = true)]
		[SMCConditionalRule("R1 || R2 || R3")]
		[SMCConditionalRequired(nameof(SeqTermoIntercambio), SMCConditionalOperation.NotEqual, null, RuleName = "R4")]
		[SMCConditionalRequired(nameof(OrientacaoAluno), SMCConditionalOperation.Equals, CadastroOrientacao.Exige, RuleName = "R5")]
		[SMCConditionalRule("R4 && R5")]
		[SMCConditionalDisplay(nameof(ExisteTipoOrientacaoParametrizado), SMCConditionalOperation.Equals, true)]
		[SMCDependency(nameof(SeqTermoIntercambio), "PreencherDadosTermo", "RealizarAtendimento", "SRC", false, nameof(SeqPessoaAtuacao))]
		public long? SeqTipoOrientacao { get; set; }

		[SMCDetail(ClearOnChangeProperty = nameof(SeqTipoOrientacao))]
		[SMCSize(SMCSize.Grid24_24)]
		[SMCConditionalDisplay(nameof(SeqTipoOrientacao), SMCConditionalOperation.NotEqual, "")]
		[SMCConditionalReadonly(nameof(CotutelaJaAssociada), true, PersistentValue = true)]
		public SMCMasterDetailList<SolicitacaoIntercambioParticipanteViewModel> Participantes { get; set; }

		[SMCHidden]
		public long? SeqOrientacao { get; set; }
	}
}