using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ConsultaPublicaHistoricoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumentoConclusao { get; set; }

        public long? SeqSituacaoDocumentoAcademico { get; set; }

        public string DescricaoSituacaoDocumentoAcademico { get; set; }

        public DateTime DataInclusao { get; set; }

        public string Token { get; set; }

        public string DescricaoClassificacaoInvalidadeDocumento { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }

        public string PeriodoInvalidade { get; set; }
    }
}
