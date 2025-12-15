using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class RegistroDocumentoAtendimentoData : ISMCMappable
    {
        public bool NecessitaConfirmacaoEntregaDocumentos { get; set; }
        public List<string> DocumentosAlterados { get; set; }
        public List<RegistroDocumentoAtendimentoDocumentoData> Documentos { get; set; }
    }
}