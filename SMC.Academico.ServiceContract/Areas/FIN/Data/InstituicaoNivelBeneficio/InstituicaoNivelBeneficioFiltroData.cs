using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class InstituicaoNivelBeneficioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqBeneficio { get; set; }
    }
}
