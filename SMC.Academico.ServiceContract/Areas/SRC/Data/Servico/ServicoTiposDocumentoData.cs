using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoTiposDocumentoData : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqServico { get; set; }
        public long SeqTipoDocumento { get; set; }
        public string DescricaoXSD { get; set; }
    }
}