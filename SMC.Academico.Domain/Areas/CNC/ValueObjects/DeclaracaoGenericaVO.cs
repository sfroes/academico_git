using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DeclaracaoGenericaVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqTipoDocumentoAcademico { get; set; }
        public short NumeroViaDocumento { get; set; }
        public long? SeqDocumentoAcademicoViaAnterior { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public long? SeqDocumentoAcademicoHistoricoSituacaoAtual { get; set; }
        public long? SeqDocumentoGAD { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        public long? SeqPessoaDadosPessoais { get; set; }
    }
}
