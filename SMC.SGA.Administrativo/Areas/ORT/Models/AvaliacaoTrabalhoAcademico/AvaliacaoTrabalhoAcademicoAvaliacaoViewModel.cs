using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
	public class AvaliacaoTrabalhoAcademicoAvaliacaoViewModel : SMCViewModelBase
	{
		#region [ Hidden ]

		[SMCHidden]
		public long Seq { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexadoAtaDefesa { get; set; }

		[SMCHidden]
		public string GuidArquivoAnexadoAtaDefesa { get; set; }

		[SMCHidden]
		public bool AlunoFormado { get; set; }

		[SMCHidden]
		public long SeqOrigemAvaliacao { get; set; }

		[SMCHidden]
		public long SeqTrabalhoAcademico { get; set; }

		[SMCHidden]
		public DateTime? DataCancelamento { get; set; }

		[SMCHidden]
		public bool BancaEstaCancelada { get { return DataCancelamento.HasValue; } }

		[SMCHidden]
		public bool MostrarNotaConceito { get { return !string.IsNullOrEmpty(NotaConceito); } }

		[SMCHidden]
		public TipoAvaliacao TipoAvaliacao { get; set; }

		[SMCHidden]
		public bool MostrarMembrosBancaExaminadora { get { return this.TipoAvaliacao == TipoAvaliacao.Banca; } }

		[SMCHidden]
		public DateTime? DataAutorizacaoSegundoDeposito { get; set; }

		/// <summary>
		/// Este comando estará disponível somente para avaliações cuja a
		/// apuração ainda não tenha sido realizada. Ou seja, sem nota ou conceito lançados.
		/// </summary>
		[SMCHidden]
		public bool PermitirExlusaoAvaliacao { get; set; }

		/// <summary>
		/// Este comando stará disponível somente se a apuração tiver sido apurada, ou seja,
		/// com nota ou conceito já lançados.
		/// E a situação da formação do aluno for diferente de "Formado", "Cancelado" ou "Não Optado".
		/// E se não houver registro de publicação no BDP cuja a data de publicação já tenha sido informada.
		/// </summary>
		[SMCHidden]
		public bool PermitirExclusaoNotaApuracao { get; set; }

		/// <summary>
		/// Este comando estará disponível somente se a apuração não tiver
		/// sido apurada, ou seja, com nota ou conceito já lançados.
		/// E a situação atual da formação do aluno for  diferente de "Formado", "Cancelado" ou "Não Optado".
		/// E para avaliações cuja a data seja menor ou igual a data atual.
		/// E para avaliação que Nâo tenha sido cancelada.
		/// </summary>
		[SMCHidden]
		public bool PermitirLancarNota { get; set; }

        [SMCHidden]
		public BancaCancelada Legenda
		{
			get { return BancaEstaCancelada ? BancaCancelada.Sim : BancaCancelada.Nao; }
		}

		[SMCHidden]
		public List<string> DescricoesMembros { get => MembrosBancaExaminadora?.Select(m => m.DescricaoMembro).ToList(); }

		[SMCHidden]
		public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

		#endregion [ Hidden ]

		[SMCDataSource]
		[SMCHidden]
		public List<SMCDatasourceItem> TiposMembroBanca { get; set; }

		[SMCReadOnly]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
		public DateTime DataInicioAplicacaoAvaliacao { get; set; }

		[SMCReadOnly]
		[SMCTimeFormat(SMCTimeFormat.Default)]
		[SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid2_24)]
		public string Hora { get { return DataInicioAplicacaoAvaliacao.ToShortTimeString(); } }

		[SMCReadOnly]
		[SMCMaxLength(100)]
		[SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid14_24)]
		public string Local { get; set; }

		[SMCMaxLength(15)]
		[SMCReadOnly]
		[SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
		public string NotaConceito { get; set; }

		[SMCReadOnly]
		[SMCMaxLength(100)]
		[SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid14_24)]
		public string AtaDefesa { get; set; }

		[SMCHidden]
        public bool PublicacaoBiblioteca { get; set; }

		[SMCHidden]
        public bool JaApurado { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
		public List<MembroBancaExaminadoraViewModel> MembrosBancaExaminadora { get; set; }
	}
}