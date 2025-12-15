using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data.Periodico
{
    public class PeriodicoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }

       public long? SeqClassificacaoPeriodico { get; set; }

        public int? AnoInicio { get; set; }

        public int? AnoFim { get; set; }

        public string Descricao { get; set; }

        public string CodigoISSN { get; set; }

        public string DescAreaAvaliacao { get; set; }

        public QualisCapes? QualisCapes { get; set; }

        public bool? ClassificacaoPeriodicoAtual { get; set; }
    }
}
