using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class RegistroDocumentoAtendimentoDocumentoData : ISMCMappable
    {
        public List<DocumentoAtendimentoData> Documentos { get; set; }
    }
}