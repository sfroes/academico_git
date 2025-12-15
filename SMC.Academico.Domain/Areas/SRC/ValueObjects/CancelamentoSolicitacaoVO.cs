using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CancelamentoSolicitacaoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long? SeqMotivoCancelamento { get; set; }

        public string TokenMotivoCancelamento { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public MotivoInvalidadeDocumento? MotivoInvalidadeDocumento { get; set; }

        public string Observacao { get; set; }

        public string ObservacaoDocumentoGAD { get; set; }

        public TipoInvalidade TipoCancelamento { get; set; }

        public long? SeqClassificacaoInvalidadeDocumento { get; set; }
    }
}