using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
	public class SolicitacaoAtividadeComplementarPaginaData : ISMCMappable, ISMCLookupData
	{
		public long SeqSolicitacaoServico { get; set; }

		public long SeqTipoDivisaoComponente { get; set; }

		public long SeqDivisaoComponente { get; set; }

		public long? SeqCicloLetivo { get; set; }

		public string Descricao { get; set; }

		public short? CargaHoraria { get; set; }

		public DateTime? DataInicio { get; set; }

		public DateTime? DataFim { get; set; }

		public TipoPublicacao? TipoPublicacao { get; set; }

		#region [ Tipo Publicação - Conferência ]

		public string DescricaoEvento { get; set; }

		public int? AnoRealizacaoEvento { get; set; }

		public NaturezaArtigo? NaturezaArtigo { get; set; }

		public TipoEvento? TipoEvento { get; set; }

		[SMCMapProperty("EstadoCidade.Estado")]
		public string UfEvento { get; set; }

		[SMCMapProperty("EstadoCidade.SeqCidade")]
		public int? CodCidadeEvento { get; set; }

		#endregion [ Tipo Publicação - Conferência ]

		#region [ Tipo Publicação - Periódico ]

		public long? SeqPeriodico { get; set; }

		public int? NumeroVolumePeriodico { get; set; }

		public int? NumeroFasciculoPeriodico { get; set; }

		public int? NumeroPaginaInicialPeriodico { get; set; }

		public int? NumeroPaginaFinalPeriodico { get; set; }

		#endregion [ Tipo Publicação - Periódico ]

		#region [ Lançamento de Nota ]

		public decimal? Nota { get; set; }

		public long? SeqEscalaApuracaoItem { get; set; }

		public int? Faltas { get; set; }

		public SituacaoHistoricoEscolar? SituacaoFinal { get; set; }

		#endregion [ Lançamento de Nota ]
	}
}