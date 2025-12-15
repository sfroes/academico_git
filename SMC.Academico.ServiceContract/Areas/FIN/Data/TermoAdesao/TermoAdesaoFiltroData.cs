using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqContrato { get; set; }

        public string Titulo { get; set; }
         
        public bool? Ativo { get; set; }
    }
}
