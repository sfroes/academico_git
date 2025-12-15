using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaHistoricoVigenciaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDispensa { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid11_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid11_24)]
        public DateTime? DataFimVigencia { get; set; }
    }
}