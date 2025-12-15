using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class QualisPeriodicoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPeriodico { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string CodigoISSN { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCCssClass("smc-size-md-16 smc-size-xs-16 smc-size-sm-16 smc-size-lg-16")]
        public string DescricaoAreaAvaliacao { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSize(SMCSize.Grid8_24)]
        public QualisCapes QualisCapes { get; set; }
    }
}