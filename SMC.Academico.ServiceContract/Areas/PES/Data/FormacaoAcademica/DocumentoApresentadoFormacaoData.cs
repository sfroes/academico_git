using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class DocumentoApresentadoFormacaoData : ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqFormacaoAcademica { get; set; }

        public long? SeqTitulacaoDocumentoComprobatorio { get; set; }
    }
}
