using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class LancamentoNotaBancaExaminadoraVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqAplicacaoAvaliacao { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public long? SeqTrabalhoAcademico { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public bool? ApuracaoNota { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public decimal? Nota { get; set; }

		public SMCUploadFile ArquivoAnexadoAtaDefesa { get; set; }

		public bool? CriterioAprovacaoAprovado { get; set; }

        public DateTime? DataAvaliacao { get; set; }

        public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

        public List<MembroBancaExaminadoraVO> MembrosBancaExaminadora { get; set; }

        public decimal? NotaMaxima { get; set; }

		public long? SeqInstituicaoEnsino { get; set; }

		public long SeqTipoTrabalho { get; set; }
		public bool PublicacaoBibliotecaObrigatoria { get; set; }

        public int? NumeroDefesa { get; set; }
    }
}