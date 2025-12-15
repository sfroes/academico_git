using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class BeneficioHistoricoValorAuxilioFitroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivelBeneficio { get; set; }
    }
}
