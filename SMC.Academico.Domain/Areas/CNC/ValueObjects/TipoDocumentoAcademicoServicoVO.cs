using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class TipoDocumentoAcademicoServicoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public long SeqServico { get; set; }
    }
}
