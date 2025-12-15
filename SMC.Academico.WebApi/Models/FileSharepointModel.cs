using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.Academico.WebApi.Models
{
    public class FileSharepointModel
    {
        public string biblioteca { get; set; }
        public MetadadoSharepointModel metadado { get; set; }
        public ArquivoSharepointModel arquivo { get; set; }
    }

    public class MetadadoSharepointModel 
    {
        public string codigo { get; set; }
        public string sistema { get; set; }
        public string nome { get; set; }
    }

    public class ArquivoSharepointModel
    {
        public string pasta { get; set; }
        public string nome { get; set; }
        public string extensao { get; set; }
        public byte[] conteudo { get; set; }
    }

    public class RetornoSharepointModel
    {
        public string IdDocumento { get; set; }
        public string LinkDocumento { get; set; }
    }
}