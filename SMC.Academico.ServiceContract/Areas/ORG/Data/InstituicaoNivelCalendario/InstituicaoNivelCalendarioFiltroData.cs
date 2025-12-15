using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelCalendarioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivel { get; set; }
        public UsoCalendario? UsoCalendario { get; set; }
    }
}
