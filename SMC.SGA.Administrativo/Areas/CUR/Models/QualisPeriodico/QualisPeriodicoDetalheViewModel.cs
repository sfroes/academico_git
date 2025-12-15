using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class QualisPeriodicoDetalheViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPeriodico { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCRequired]
        [SMCUnique]
        public string DescricaoAreaAvaliacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCUnique]
        public string CodigoISSN { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCRequired]
        [SMCUnique]
        public QualisCapes QualisCapes { get; set; }
    }
}