using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TipoDocumentoAcademicoServicoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoDocumentoAcademico { get; set; }

        public long SeqServico { get; set; }
    }
}
