using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class EntregaDocumentoDigitalUploadPaginaData : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public string Descricao { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}
