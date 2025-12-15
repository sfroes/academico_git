using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public bool LancamentoHistorico { get; set; }

        public long SeqAtuacaoAluno { get; set; }

        public short NumeroViaDocumento { get; set; }

        public long? SeqDocumentoConclusaoViaAnterior { get; set; }

        public short TipoPapel { get; set; }

        public long? SeqPessoaDadosPessoais { get; set; }

        public DateTime? DataImpressao { get; set; }

        public string UsuarioImpressao { get; set; }

        public long? NumeroSerie { get; set; }

        public TipoRegistroDocumento? TipoRegistroDocumento { get; set; }

        public string NumeroProcesso { get; set; }

        public string NumeroRegistro { get; set; }

        public DateTime? DataRegistro { get; set; }

        public string UsuarioRegistro { get; set; }

        public string Livro { get; set; }

        public string Folha { get; set; }

        public long? SeqDocumentoAcademicoHistoricoSituacaoAtual { get; set; }

        public long? SeqSolicitacaoDocumentoConclusao { get; set; }

        public string CodigoMigracaoDocumento { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public long? SeqDocumentoDiplomaGAD { get; set; }
    }
}
