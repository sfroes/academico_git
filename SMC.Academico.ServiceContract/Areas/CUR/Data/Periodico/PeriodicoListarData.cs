using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class PeriodicoListarData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public string Descricao { get; set; }

        public ClassificacaoPeriodicoData ClassificacaoPeriodico { get; set; }

        public List<QualisPeriodicoData> QualisPeriodico { get; set; }
    }
}
