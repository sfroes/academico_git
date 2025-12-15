using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoDocumentoRequeridoItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqGrupoDocumentoRequerido { get; set; }

        public long SeqDocumentoRequerido { get; set; }
    }
}
