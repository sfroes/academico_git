using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EntregaDocumentoDigitalUploadPaginaVO : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public string Descricao { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}
