using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class DocumentoApresentadoFormacaoVO : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqFormacaoAcademica { get; set; }

        public long? SeqTitulacaoDocumentoComprobatorio { get; set; }
    }
}

