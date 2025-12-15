using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoOfertaFiltroViewModel : SMCPagerViewModel
    {
        public List<SMCSelectListItem> TiposOferta { get; set; }

        public long SeqCampanha { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        [SMCSelect(nameof(TiposOferta), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoOferta { get; set; }

        [SMCSize(SMCSize.Grid16_24)]
        public string Oferta { get; set; }

        public long[] Seqs { get; set; }
    }
}