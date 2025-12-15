using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class AtoNormativoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCSize(SMCSize.Grid1_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqAssuntoNormativo { get; set; }

        [SMCMapProperty("AssuntoNormativo.Descricao")]
        [SMCSortable(true, false, "AssuntoNormativo.Descricao")]
        public string DescricaoAssuntoNormativo { get; set; }

        [SMCHidden]
        public long SeqTipoAtoNormativo { get; set; }

        [SMCMapProperty("TipoAtoNormativo.Descricao")]
        [SMCSortable(true, false, "TipoAtoNormativo.Descricao")]
        public string DescricaoTipoAtoNormativo { get; set; }

        [SMCSortable(true, false)]
        public string NumeroDocumento { get; set; }

        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public DateTime DataDocumento { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }
    }
}