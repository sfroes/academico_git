using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoHistoricoListarVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }

        public DateTime DataInclusao { get; set; }

        public string UsuarioInclusao { get; set; }

        public string Observacao { get; set; }

        public MotivoInvalidadeDocumento? MotivoInvalidadeDocumento { get; set; }

        public string MotivoInvalidadeObservacao { get; set; }

        public string Token { get; set; }

        public string DescricaoClassificacaoInvalidadeDocumento { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }
    }
}
