using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DocumentoAcademicoCurriculoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqTipoDocumentoAcademico { get; set; }
        public short NumeroViaDocumento { get; set; }
        public long? SeqDocumentoAcademicoViaAnterior { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public long? SeqDocumentoAcademicoHistoricoSituacaoAtual { get; set; }
        public long? SeqDocumentoGAD { get; set; }
        public long? SeqCurriculo { get; set; }
    }
}
