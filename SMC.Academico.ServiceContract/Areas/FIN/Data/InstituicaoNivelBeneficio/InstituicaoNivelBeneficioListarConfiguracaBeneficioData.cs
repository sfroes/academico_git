using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class InstituicaoNivelBeneficioListarConfiguracaBeneficioData : SMCPagerFilterData, ISMCMappable
    {

        public long Seq { get; set; }
         
        public TipoDeducao TipoDeducao { get; set; }

        public FormaDeducao FormaDeducao { get; set; }

        public decimal? ValorDeducao { get; set; }

        public DateTime DataInicioValidade { get; set; }

        public DateTime? DataFimValidade { get; set; }

        public bool IncluirDesbloqueioTemporario { get; set; }
    }
}
