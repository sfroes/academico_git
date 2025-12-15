using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TipoDocumentoAcademicoTagVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public long SeqTag { get; set; }

        public bool? PermiteEditarDado { get; set; }

        public bool? TipoReadOnly { get; set; }

        public string InformacaoTag { get; set; }

        public TipoPreenchimentoTag TipoPreenchimentoTag { get; set; }

        public string DescricaoTag { get; set; }

        public string QueryOrigem { get; set; }
    }
}
