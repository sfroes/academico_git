using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
	public class AlunoLookupFiltroViewModel : SMCLookupFilterViewModel
	{
		#region [ DataSources ]

		public List<SMCDatasourceItem> Turnos { get; set; }

		public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

		public List<SMCDatasourceItem> TipoVinculoAluno { get; set; }

		/// <summary>
		/// Entidades resposáveis por curso segundo BI_CSO_002.NV01
		/// </summary>
		public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

		public List<SMCDatasourceItem> Localidades { get; set; }

		public List<SMCDatasourceItem> NiveisEnsino { get; set; }

		#endregion [ DataSources ]

		[SMCFilter(true, true)]
		[SMCKey]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
		public long? NumeroRegistroAcademico { get; set; }

		[SMCDescription]
		[SMCFilter(true, true)]
		[SMCMaxLength(100)]
		[SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid9_24)]
		public string Nome { get; set; }

		[SMCConditionalReadonly(nameof(SeqNivelEnsinoReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
		[SMCFilter(true, true)]
		[SMCSelect(nameof(NiveisEnsino))]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
		public long? SeqNivelEnsino { get; set; }

		[SMCConditionalReadonly(nameof(SeqTipoVinculoAlunoReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
		[SMCFilter(true, true)]
		[SMCSelect(nameof(TipoVinculoAluno))]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
		public long? SeqTipoVinculoAluno { get; set; }

		[SMCConditionalReadonly(nameof(VinculoAlunoAtivoReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
		[SMCFilter(true, true)]
		[SMCSelect]
		[SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
		public bool? VinculoAlunoAtivo { get; set; }

		#region Hidden

		[SMCHidden]
		public bool SeqNivelEnsinoReadOnly { get; set; }

		[SMCHidden]
		public bool SeqTipoVinculoAlunoReadOnly { get; set; }

		[SMCHidden]
		public bool VinculoAlunoAtivoReadOnly { get; set; }

		[SMCHidden]
		public bool? AlunoDI { get; set; }

		[SMCHidden]
		[SMCKey]
		public long? SeqAluno { get; set; }

		#endregion Hidden

		#region [BI_CSO_001]

		/// <summary>
		/// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
		/// e mapeado para o nome adequando para os dtos
		/// </summary>
		[SMCFilter(true, true)]
		[SMCMapProperty("SeqEntidadesResponsaveis")]
		[SMCSelect(nameof(EntidadesResponsaveis))]
		[SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
		public List<long> SeqsEntidadesResponsaveis { get; set; }

		[SMCFilter(true, true)]
		[SMCSelect(nameof(Localidades), autoSelectSingleItem: true)]
		[SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
		public long? SeqLocalidade { get; set; }

		[SMCHidden]
		public long? SeqCursoOfertaLocalidade { get; set; }

		[CursoOfertaLookup]
		[SMCDependency(nameof(SeqsEntidadesResponsaveis))]
		[SMCDependency(nameof(SeqLocalidade))]
		[SMCDependency(nameof(SeqNivelEnsino))]
		[SMCDependency(nameof(EntidadesAtivas))]
		[SMCDependency(nameof(RetornarTodos))]
		[SMCFilter(true, true)]
		[SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid7_24)]
		public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

		[SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "Rule1")]
		[SMCConditionalReadonly(nameof(SeqLocalidade), SMCConditionalOperation.Equals, "", RuleName = "Rule2")]
		[SMCConditionalRule("Rule1 && Rule2")]
		[SMCDependency(nameof(SeqCursoOferta), "BuscarTurnosPorCursoOfertaOuLocalidadeSelect", "Turno", "CSO", false, includedProperties: new[] { nameof(SeqLocalidade) })]
		[SMCDependency(nameof(SeqLocalidade), "BuscarTurnosPorCursoOfertaOuLocalidadeSelect", "Turno", "CSO", false, includedProperties: new[] { nameof(SeqCursoOferta) })]
		[SMCFilter(true, true)]
		[SMCSelect(nameof(Turnos))]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
		public long? SeqTurno { get; set; }

		[SMCHidden]
		public bool EntidadesAtivas { get; set; } = false;

		[SMCHidden]
		public long? SeqCursoOfertaParam
		{
			get { return SeqCursoOferta?.Seq; }
			set
			{
				if (value != null)
				{
					SeqCursoOferta = SeqCursoOferta ?? new CursoOfertaLookupViewModel();
					SeqCursoOferta.Seq = value;
				}
				else
					SeqCursoOferta = null;
			}
		}

		/// <summary>
		/// Propriedade que trata o retorno de todos os graus academicos
		/// </summary>
		[SMCHidden]
		private bool RetornarTodos { get; set; } = true;

		[SMCHidden]
		public long? SeqTipoTermoIntercambio { get; set; }

		[SMCHidden]
		public bool TelaPesquisa { get; set; }

		#endregion [BI_CSO_001]
	}
}