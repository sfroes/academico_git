using SMC.Academico.Common.Areas.CNC.Models;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    class RetornoDocumentoVO : MensagemHttp
    {
        public List<DocumentoGadVO> Itens { get; set; }
    }
}
