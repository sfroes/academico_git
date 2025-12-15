using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
   public class QualisPeriodicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string CodigoISSN { get; set; }

        public long SeqPeriodico { get; set; }

        public string DescricaoAreaAvaliacao { get; set; }

        public QualisCapes QualisCapes { get; set; }
    }
}
