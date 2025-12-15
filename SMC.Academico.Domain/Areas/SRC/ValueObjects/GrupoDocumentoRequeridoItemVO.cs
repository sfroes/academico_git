using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoRequeridoItemVO : ISMCMappable
    {        
        public long Seq { get; set; }

        public long SeqGrupoDocumentoRequerido { get; set; }

        public long SeqDocumentoRequerido { get; set; }
    }
}
