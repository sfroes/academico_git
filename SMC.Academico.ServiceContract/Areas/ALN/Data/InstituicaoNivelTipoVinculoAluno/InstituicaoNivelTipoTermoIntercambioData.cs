using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoTermoIntercambioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }
         
        [SMCMapProperty("ConcedeFormacao")]
        public bool? ConcedeFormacaoTipoTermoIntercambio { get; set; }
         
        public bool? ExigePeriodoIntercambioTermo { get; set; } 
         
        public bool? PermiteIngresso { get; set; }
         
        public bool? PermiteSaidaIntercambio { get; set; }

        public long SeqNivelEnsino { get; set; }
    }
}
