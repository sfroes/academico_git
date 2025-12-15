using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AtoNormativoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqAssuntoNormativo { get; set; }

        [SMCMapProperty("AssuntoNormativo.Descricao")]
        public string DescricaoAssuntoNormativo { get; set; }

        public long SeqTipoAtoNormativo { get; set; }

        [SMCMapProperty("TipoAtoNormativo.Descricao")]
        public string DescricaoTipoAtoNormativo { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime DataDocumento { get; set; }

        public string Descricao { get; set; }
    }
}
