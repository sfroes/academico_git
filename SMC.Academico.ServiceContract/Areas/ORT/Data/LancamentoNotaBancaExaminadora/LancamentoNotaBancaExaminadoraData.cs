using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
	public class LancamentoNotaBancaExaminadoraData : ISMCMappable
	{
		public long Seq { get; set; }

		public long? SeqAplicacaoAvaliacao { get; set; }

		public long? SeqEscalaApuracaoItem { get; set; }

		public long? SeqTrabalhoAcademico { get; set; }

		public long? SeqComponenteCurricular { get; set; }

		public long? SeqOrigemAvaliacao { get; set; }

		public long? SeqNivelEnsino { get; set; }

		public bool? ApuracaoNota { get; set; }

		public long? SeqEscalaApuracao { get; set; }

		public decimal? Nota { get; set; }

		public SMCUploadFile ArquivoAnexadoAtaDefesa { get; set; }

		public long? SeqConceito { get; set; }

		public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

		public List<MembroBancaExaminadoraData> MembrosBancaExaminadora { get; set; }

		public decimal? NotaMaxima { get; set; }

		public long? SeqInstituicaoEnsino { get; set; }

		public long SeqTipoTrabalho { get; set; }
		public bool PublicacaoBibliotecaObrigatoria { get; set; }
        public int? NumeroDefesa { get; set; }

    }
}