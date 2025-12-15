using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoDocumentoRequeridoItemListarData : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}
