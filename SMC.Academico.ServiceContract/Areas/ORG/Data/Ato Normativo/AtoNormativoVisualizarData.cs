using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AtoNormativoVisualizarData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("AssuntoNormativo.Descricao")]
        public string DescricaoAssuntoNormativo { get; set; }

        [SMCMapProperty("TipoAtoNormativo.Descricao")]
        public string DescricaoTipoAtoNormativo { get; set; }

        public string NumeroDocumento { get; set; }

        [SMCMapProperty("AtoNormativo.DataDocumento")]
        public DateTime DataDocumento { get; set; }

        public List<string> GrauAcademico { get; set; }

        public string DescricaoGrauAcademico { get; set; }
    }
}
